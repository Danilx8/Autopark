using Autopark.Models;
using Autopark.Services.Vehicles;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Autopark.Areas.Manager.Pages.Map
{
    public class IndexModel(IVehiclesService _vehicles, IConfiguration _options) : PageModel
    {
        private readonly IVehiclesService Vehicles = _vehicles;
        private readonly IConfiguration Options = _options;

        public DateTime StartSample = DateTime.Today.AddYears(-1);
        public DateTime EndSample = DateTime.Today.AddDays(-7);
        public string VehicleName { get; set; }
        public int? VehicleEnterpriseId { get; set; }
        public string YandexAPI { get; set; }

        public void OnGet(int vehicleId)
        {
            Vehicle vehicle = Vehicles.FindVehicleById(vehicleId)
                ?? throw new ArgumentException("No vehicle with given id");
            VehicleName = vehicle.Name;
            VehicleEnterpriseId = vehicle.EnterpriseId;
            YandexAPI = Options["Autopark:YandexJavaScriptAPI"]!;
        }
    }
}
