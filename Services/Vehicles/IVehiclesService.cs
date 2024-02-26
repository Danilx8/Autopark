using Autopark.Models;

namespace Autopark.Services.Vehicles
{
    public interface IVehiclesService
    {
        public Vehicle? FindVehicleById(int vehicleId);
    }
}
