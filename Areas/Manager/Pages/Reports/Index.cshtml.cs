using Autopark.Models;
using Autopark.Services.Vehicles;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autopark.Areas.Manager.Pages.Reports
{
    public class IndexModel(IVehiclesService _vehicles) : PageModel
    {
        private readonly IVehiclesService vehicles = _vehicles;

        public int? VehicleEnterpriseId { get; set; }
        public DateTime Start = DateTime.Today.AddYears(-1);
        public DateTime End = DateTime.Today.AddDays(-7);
        public string VehicleName { get; set; }
        public IEnumerable<SelectListItem> Intervals { get; set; }
        public int Interval { get; set; }
        public string Authorization { get; set; }
        private static readonly string[] sourceArray = ["carmileage"];

        public void OnGet(int vehicleId, string reportType)
        {
            if (!sourceArray.Contains(reportType.ToLower()))
                throw new ArgumentException("No such report type available");
            
            Vehicle vehicle = vehicles.FindVehicleById(vehicleId)
                ?? throw new ArgumentException("No vehicle with given id");
            VehicleEnterpriseId = vehicle.EnterpriseId;
            VehicleName = vehicle.Name;
            List<SelectListItem> intervals = [];
            foreach (Interval interval in Enum.GetValues(typeof(Interval)))
            {
                intervals.Add(new SelectListItem
                {
                    Text = interval.Humanize(),
                    Value = interval.ToString("d")
                });
            };
            Intervals = intervals;

            if (!HttpContext.Request.Cookies.TryGetValue(".Autopark.Nugget", out string? authToken))
                throw new AccessViolationException("You aren't authorized to make reports");
            Authorization = authToken;
        }
    }
}
