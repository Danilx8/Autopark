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
using Autopark.Services.Vehicles;

namespace Autopark.Areas.Manager.Controllers
{
    [ApiController]
    public class GeoController(ApplicationDbContext db, IConfiguration options, IPathsService paths, 
        IVehiclesService vehicles, ILogger<GeoController> logger) : BaseManagerController(db)
    {
        [HttpGet]
        [Route("{vehicleId}")]
        public IActionResult GetPaths(
            int vehicleId, [FromQuery] DateTime start, [FromQuery] DateTime finish,
            [FromHeader] string timeZoneId, [FromHeader] bool displayGeoJson = false)
        {
            Vehicle? vehicle = vehicles.FindVehicleById(vehicleId);
            if (vehicle == null) return NoContent();
            
            if (!TimeZoneInfo.TryFindSystemTimeZoneById(timeZoneId, out TimeZoneInfo? timeZoneInfo))
                return BadRequest("Specified time zone can't be parsed");

            List<Geopoint>? path = paths.ReadAllPoints(vehicleId, start, finish);
            if (path == null) return NoContent();

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
            var rides = paths.ReadAllRides(vehicleId, start, finish);
            if (rides == null) return NoContent();

            Dictionary<int, Geopoint> starts = [];
            Dictionary<int, Geopoint> finishes = [];
            foreach (var ride in rides)
            {
                var firstPoint = paths.FindExactPoint(vehicleId, start) 
                                 ?? throw new Exception("Ride " + ride.Id + " starting point wasn't found");
                var lastPoint = paths.FindExactPoint(vehicleId, start) 
                                ?? throw new Exception("Ride " + ride.Id + " finishing point wasn't found");
                starts.Add(ride.Id, firstPoint);
                finishes.Add(ride.Id, lastPoint);
            }

            List<RidesResult> result = [];

            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU");
            string apiKey = options["Autopark:ORSDirectionsAPI"]!;
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
            var points = paths.ReadAllPoints(vehicleId, start, finish);

            return Ok(points);
        }

        [HttpPost]
        [Route("{vehicleId}")]
        [AllowAnonymous]
        public IActionResult RenderPathMap(int vehicleId, [FromBody] TimeDto time)
        {
            var rides = paths.ReadAllRides(vehicleId, time.Start, time.Finish);
            if (rides == null) return NoContent();
            
            //get rides' coordinates
            Dictionary<int, List<Geopoint>> calculatedPaths = [];
            rides.ForEach(r => calculatedPaths.Add(r.Id, paths.ReadAllPoints(vehicleId, r.Start, r.Finish, 1000)!));
            List<RideRenderInfo> info = [];
            var random = new Random();
            rides.ForEach(r =>
            {
                var color = $"#{random.Next(0x1000000):X6}";
                info.Add(new RideRenderInfo
                {
                    Ride = r,
                    Path = new LineString(calculatedPaths[r.Id].Select(p => p.Point.Coordinate).ToArray()),
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
            
            logger.LogInformation("Successfully rendered a JSON for all rides map for the vehicle with id of " +
                                  "{VehicleId} in the timespan of {@Time}", vehicleId, time);
            
            return Ok(stringWriter.ToString());
        }
    }
}
