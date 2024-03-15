using Autopark.Data;
using Autopark.Models;
using Autopark.Models.Dto;
using Autopark.Services.Paths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using System.Data;
using System.Text.Json;

namespace Autopark.Areas.Manager.Controllers
{
    public class ReportsController(ApplicationDbContext db, IPathsService paths,
        IConfiguration options) : BaseManagerController(db)
    {
        protected readonly IPathsService paths = paths;
        protected readonly IConfiguration options = options;

        [AllowAnonymous]
        [Route("{vehicleId}/{interval}")]
        public async Task<IActionResult> CreateVehiclesReport(int vehicleId, string interval,
            [FromBody] TimeDto time)
        {
            if (!Enum.TryParse(interval, out Interval _interval))
                return BadRequest("Wrong interval format");

            var rides = db
                .Rides
                .Where(r => r.VehicleId == vehicleId
                    && r.Start >= time.Start
                    && r.Finish <= time.End)
                .OrderBy(r => r.Start)
                .ToList();

            Dictionary<DateOnly, double> mileages = [];

            foreach(var ride in rides)
            {
                var startPoint = db.Points.Where(p => p.RegisterTime.Year == ride.Start.Year
                                              && p.RegisterTime.Month == ride.Start.Month
                                              && p.RegisterTime.Day == ride.Start.Day
                                              && p.RegisterTime.Hour == ride.Start.Hour
                                              && p.RegisterTime.Minute == ride.Start.Minute
                                              && p.RegisterTime.Second == ride.Start.Second)
                                    .First();
                var finishPoint = db.Points.Where(p => p.RegisterTime.Year == ride.Finish.Year
                                               && p.RegisterTime.Month == ride.Finish.Month
                                               && p.RegisterTime.Day == ride.Finish.Day
                                               && p.RegisterTime.Hour == ride.Finish.Hour
                                               && p.RegisterTime.Minute == ride.Finish.Minute
                                               && p.RegisterTime.Second == ride.Finish.Second)
                                      .First();
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
