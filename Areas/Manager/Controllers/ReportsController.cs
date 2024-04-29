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
    public class ReportsController(ApplicationDbContext db, IPathsService paths, IConfiguration options, 
        ILogger<ReportsController> logger) : BaseManagerController(db)
    {
        protected readonly IPathsService paths = paths;

        [AllowAnonymous]
        [Route("{vehicleId}/{interval}")]
        public async Task<IActionResult> CreateVehiclesReport(int vehicleId, string interval,
            [FromBody] TimeDto time)
        {
            if (!Enum.TryParse(interval, out Interval _interval))
                return BadRequest("Wrong interval format");

            var rides = paths.ReadAllRides(vehicleId, time.Start, time.Finish);

            Dictionary<DateOnly, double> mileages = [];

            foreach(var ride in rides)
            {
                var startPoint = paths.FindExactPoint(vehicleId, ride.Start);
                var finishPoint = paths.FindExactPoint(vehicleId, ride.Finish);
                if (startPoint == null || finishPoint == null) continue;
                
                var mileage = await CalculateRideMileage(startPoint.Point, finishPoint.Point);
                DateOnly date = _interval switch
                {
                    Interval.DAY => new DateOnly(ride.Start.Year, ride.Start.Month, ride.Start.Day),
                    Interval.MONTH => new DateOnly(ride.Start.Year, ride.Start.Month, 1),
                    _ => default
                };
                if (!mileages.TryGetValue(date, out double value)) mileages.Add(date, mileage);
                else mileages[date] += mileage;
            };

            logger.LogInformation("Successfully rendered a report for a vehicle with id of {vehicleId} in the " +
                                  "timespan of {@Time}", vehicleId, time);
            
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
