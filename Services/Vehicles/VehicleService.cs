using Autopark.Data;
using Autopark.Models;

namespace Autopark.Services.Vehicles
{
    public class VehicleService(ApplicationDbContext _db) : IVehiclesService
    {
        protected readonly ApplicationDbContext db = _db;

        public Vehicle? FindVehicleById(int vehicleId)
        {
            return db.Vehicles.Find(vehicleId);
        }
    }
}
