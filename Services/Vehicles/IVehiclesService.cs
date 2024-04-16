using Autopark.Models;
using Autopark.Models.Dto;

namespace Autopark.Services.Vehicles
{
    public interface IVehiclesService
    {
        public List<Vehicle> GetAllVehicles(int enterpriseId, PaginationFilter filter);
        public Vehicle? FindVehicleById(int vehicleId);
        public int? CreateVehicle(VehicleDto vehicle, List<int> usersCompanies);
        public bool Update(VehicleDto vehicle, List<int> usersCompanies);
        public bool DeleteVehicle(int vehicleId, List<int> usersCompanies);
    }
}
