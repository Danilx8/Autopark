using Autopark.Data;
using Autopark.Models;
using Autopark.Models.Dto;
using Autopark.Services.Paths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using System.Data;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace Autopark.Areas.Manager.Controllers
{
    public class ReportsController(ApplicationDbContext db, IPathsService paths,
        IConfiguration options, IMemoryCache cache) : BaseManagerController(db)
    {
        protected readonly IPathsService paths = paths;

        [AllowAnonymous]
        [Route("{vehicleId}/{interval}")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 300)]
        public async Task<IActionResult> CreateVehiclesReport(int vehicleId, string interval,
            [FromBody] TimeDto time)
        {
            if (!Enum.TryParse(interval, out Interval _interval))
                return BadRequest("Wrong interval format");

            var rides = paths.ReadAllRides(vehicleId, time.Start, time.Finish);

            Dictionary<DateOnly, double> mileages = [];

            foreach(var ride in rides)
            {
                var startPoint = paths.FindExactPoint(vehicleId, time.Start);
                var finishPoint = paths.FindExactPoint(vehicleId, time.Finish);
                if (startPoint == null || finishPoint == null) continue;
                
                var mileage = await CalculateRideMileage(startPoint.Point, finishPoint.Point);
                DateOnly date = default;
                switch (_interval)
                {
                    case Interval.DAY:
                        date = new DateOnly(ride.Start.Year, ride.Start.Month, ride.Start.Day);
                        break;
                    case Interval.MONTH:
                        date = new DateOnly(ride.Start.Year, ride.Start.Month, 1);
                        break;
                }
                if (!mileages.TryGetValue(date, out double value)) mileages.Add(date, mileage);
                else mileages[date] += mileage;
            };

            return Ok(mileages);
        }

        private async Task<double> CalculateRideMileage(Point start, Point finish)
        {
            var apiKey = options["Autopark:ORSDirectionsAPI"];
            var url = "https://api.openrouteservice.org/v2/matrix/driving-car";
            using HttpClient client = new();

            client.DefaultRequestHeaders.Add("Authorization", apiKey);
            double[][] locations = [[start.X, start.Y], [finish.X, finish.Y]];
            string[] metrics = ["distance"];

            var response = await client.PostAsJsonAsync(url, new
            {
                locations,
                metrics
            });
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode.ToString());

            using JsonDocument json = await JsonDocument.ParseAsync(
                await response.Content.ReadAsStreamAsync());
            JsonElement distance = json.RootElement.GetProperty("distances")[0][1];
            return distance.GetDouble();
        }
    }
}
