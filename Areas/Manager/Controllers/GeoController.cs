using Autopark.Data;
using Autopark.Models;
using Autopark.Models.Dto;
using Autopark.Services.Paths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System.Text.Json;

namespace Autopark.Areas.Manager.Controllers
{
    [ApiController]
    public class GeoController(ApplicationDbContext db, IConfiguration options,
        IPathsService paths) : BaseManagerController(db)
    {
        private readonly IPathsService _paths = paths;
        private readonly IConfiguration _options = options;

        [HttpGet]
        [Route("{vehicleId}")]
        public IActionResult GetPaths(
            int vehicleId, [FromQuery] DateTime start, [FromQuery] DateTime finish,
            [FromHeader] string timeZoneId, [FromHeader] bool displayGeoJson = false)
        {
            Vehicle vehicle = _db.Vehicles.Find(vehicleId)!;
            if (vehicle == null)
            {
                return BadRequest("Vehicle id is invalid");
            }

            if (!TimeZoneInfo.TryFindSystemTimeZoneById(timeZoneId, out TimeZoneInfo? timeZoneInfo))
                return BadRequest("Specified time zone can't be parsed");

            List<Geopoint> path = [.. _db
                .Points
                .Where(p => p.VehicleId == vehicle.Id
                    && p.RegisterTime >= start
                    && p.RegisterTime <= finish)];

            path.ForEach(p => p.RegisterTime = TimeZoneInfo.ConvertTimeFromUtc(p.RegisterTime, timeZoneInfo));

            if (!displayGeoJson) return Ok(path);

            var geoSerializer = GeoJsonSerializer.Create();
            using var geoStringWriter = new StringWriter();
            using var geoJsonWriter = new JsonTextWriter(geoStringWriter);
            geoSerializer.Serialize(geoJsonWriter, path);

            return Ok(geoStringWriter.ToString());
        }

        [HttpGet]
        [Route("{vehicleId}/{start}/{finish}")]
        public async Task<IActionResult> FindRides(int vehicleId, DateTime start, DateTime finish)
        {
            var rides = _db.Rides.
                Where(r => r.VehicleId == vehicleId
                && r.Start >= start
                && r.Finish <= finish)
                .OrderBy(r => r.Start)
                .ToList();

            Dictionary<int, Geopoint> starts = [];
            Dictionary<int, Geopoint> finishes = [];
            foreach (var ride in rides)
            {
                var firstPoint = _db.Points
                    .Where(p => ride.Start.Date == p.RegisterTime.Date
                        && ride.Start.Hour == p.RegisterTime.Hour
                        && ride.Start.Minute == p.RegisterTime.Minute
                        && ride.Start.Second == p.RegisterTime.Second)
                    .FirstOrDefault()
                    ?? throw new Exception(
                        "Ride " + ride.Id + " starting point wasn't found");
                var lastPoint = _db.Points
                    .Where(p => ride.Finish.Date == p.RegisterTime.Date
                        && ride.Finish.Hour == p.RegisterTime.Hour
                        && ride.Finish.Minute == p.RegisterTime.Minute
                        && ride.Finish.Second == p.RegisterTime.Second)
                    .FirstOrDefault()
                    ?? throw new Exception(
                        "Ride " + ride.Id + " finishing point wasn't found");
                starts.Add(ride.Id, firstPoint);
                finishes.Add(ride.Id, lastPoint);
            }

            List<RidesResult> result = [];

            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU");
            string apiKey = _options["Autopark:ORSDirectionsAPI"]!;
            string orsUrl = "https://api.openrouteservice.org/geocode/reverse?size=1&api_key=" + apiKey;
            foreach (var ride in rides)
            {
                string startUrl = orsUrl + "&point.lon=" + starts[ride.Id].Point.X
                    + "&point.lat=" + starts[ride.Id].Point.Y;
                string finishUrl = orsUrl + "&point.lon=" + finishes[ride.Id].Point.X
                    + "&point.lat=" + finishes[ride.Id].Point.Y;

                var startResponse = await client.GetAsync(startUrl);
                var finishResponse = await client.GetAsync(startUrl);

                if (!(startResponse.IsSuccessStatusCode && finishResponse.IsSuccessStatusCode))
                    throw new Exception("Couldn't find rides edges locations");

                using JsonDocument jsonStart = await JsonDocument.ParseAsync(
                    await startResponse.Content.ReadAsStreamAsync());
                using JsonDocument jsonFinish = await JsonDocument.ParseAsync(
                    await finishResponse.Content.ReadAsStreamAsync());
                JsonElement startLabel = jsonStart.RootElement
                    .GetProperty("features")[0].GetProperty("properties")
                    .GetProperty("label");
                JsonElement finishLabel = jsonStart.RootElement
                    .GetProperty("features")[0].GetProperty("properties")
                    .GetProperty("label");

                result.Add(new RidesResult
                {
                    RideId = ride.Id,
                    VehicleId = ride.VehicleId,
                    StartTime = ride.Start,
                    StartLocation = startLabel.ToString(),
                    FinishTime = ride.Finish,
                    FinishLocation = finishLabel.ToString()
                });
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{vehicleId}/{start}/{finish}")]
        public IActionResult FindPoints(int vehicleId, DateTime start, DateTime finish)
        {
            var points = _paths.ReadAllPoints(vehicleId, start, finish);

            return Ok(points);
        }

        [HttpPost]
        [Route("{vehicleId}")]
        [AllowAnonymous]
        public IActionResult RenderPathMap(int vehicleId, [FromBody] TimeDto time)
        {
            var rides = _db.Rides.
                Where(r => r.VehicleId == vehicleId
                    && r.Start >= time.Start
                    && r.Finish <= time.End)
                .OrderBy(r => r.Start)
                .ToList();

            //get rides' coordinates
            Dictionary<int, List<Geopoint>> paths = [];
            rides.ForEach(r => paths.Add(r.Id, _paths.ReadAllPoints(vehicleId, r.Start, r.Finish)));
            List<RideRenderInfo> info = [];
            var random = new Random();
            rides.ForEach(r =>
            {
                var color = string.Format("#{0:X6}", random.Next(0x1000000));
                info.Add(new RideRenderInfo
                {
                    Ride = r,
                    Path = new LineString(paths[r.Id].Select(p => p.Point.Coordinate).ToArray()),
                    Color = color
                });
            });

            var center = info[0].Path.Centroid.Coordinate;
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var response = new RidesVisualInfo
            {
                CenterCoordinates = geometryFactory.CreatePoint(center),
                Rides = info
            };

            var serializer = GeoJsonSerializer.Create();
            using var stringWriter = new StringWriter();
            using var jsonWriter = new JsonTextWriter(stringWriter);
            serializer.Serialize(jsonWriter, response);
            return Ok(stringWriter.ToString());
        }
    }
}
