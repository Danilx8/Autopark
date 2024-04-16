using Autopark.Data;
using Autopark.Models;
using Microsoft.Extensions.Caching.Memory;
using Autopark.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Autopark.Services.Vehicles
{
    public class VehicleService(ApplicationDbContext db, IMemoryCache cache) : IVehiclesService
    {
        public List<Vehicle> GetAllVehicles(int enterpriseId, PaginationFilter filter)
        {
            return db
                .Vehicles
                .Where(v => v.EnterpriseId == enterpriseId)
                .Skip((filter.PageNum - 1) * filter.Limit)
                .Take(filter.Limit)
                .ToList();
        }
        
        public Vehicle? FindVehicleById(int vehicleId)
        {
            if (cache.TryGetValue(vehicleId, out Vehicle? vehicle)) return vehicle;
            vehicle = db.Vehicles.Find(vehicleId);
            if (vehicle != null)
            {
                cache.Set(vehicleId, vehicle,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }

            return vehicle;
        }

        public int? CreateVehicle(VehicleDto vehicle, List<int> usersCompanies)
        {
            vehicle.EnterpriseId ??= usersCompanies[0];
            if (!usersCompanies.Contains(vehicle.EnterpriseId ?? throw new Exception())) return null;
            
            Brand? brand = db
                .Brands
                .FirstOrDefault(b => b.Id == vehicle.BrandId);

            if (brand == null) return null;

            Enterprise? enterprise = db
                .Enterprises
                .FirstOrDefault(e => e.Id == vehicle.EnterpriseId);

            if (enterprise == null) return null;

            Vehicle newVehicle = new()
            {
                Name = vehicle.Name!,
                Price = vehicle.Price,
                ZeroToHundred = vehicle.ZeroToHundred,
                Mileage = vehicle.Mileage,
                Year = vehicle.Year,
                AcquireTime = DateTime.Parse(vehicle.AcquireTime),
                HorsePower = vehicle.HorsePower,
                BrandId = vehicle.BrandId,
                EnterpriseId = vehicle.EnterpriseId,
                DriverId = (int)vehicle.DriverId!
            };

            db.Vehicles.Add(newVehicle);
            
            int number = db.SaveChanges();
            if (number > 0)
            {
                cache.Set(newVehicle.Id, newVehicle, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }

            return newVehicle.Id;
        }

        public bool Update(VehicleDto vehicle, List<int> usersCompanies)
        {
            Brand? brand = db
                .Brands
                .FirstOrDefault(b => b.Id == vehicle.BrandId);

            if (brand == null) return false;

            Enterprise? enterprise = db
                .Enterprises
                .FirstOrDefault(e => e.Id == vehicle.EnterpriseId);

            if (enterprise == null) return false;

            if (!usersCompanies.Contains((int)vehicle.EnterpriseId!)) return false; 
            
            db
                .Vehicles
                .Where(v => v.Id == vehicle.Id)
                .ExecuteUpdate(s => s
                    .SetProperty(v => v.Name, v => vehicle.Name)
                    .SetProperty(v => v.Price, v => vehicle.Price)
                    .SetProperty(v => v.ZeroToHundred, v => vehicle.ZeroToHundred)
                    .SetProperty(v => v.Mileage, v => vehicle.Mileage)
                    .SetProperty(v => v.Year, v => vehicle.Mileage)
                    .SetProperty(v => v.HorsePower, v => vehicle.HorsePower)
                    .SetProperty(v => v.BrandId, v => vehicle.BrandId)
                    .SetProperty(v => v.EnterpriseId, v => vehicle.EnterpriseId)
                    .SetProperty(v => v.DriverId, v => vehicle.DriverId));
            int number = db.SaveChanges();
            if (number > 0)
            {
                cache.Set(vehicle.Id, db.Vehicles.First(v => v.Id == vehicle.Id), new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            
            return true;
        }
        
        public bool DeleteVehicle(int vehicleId, List<int> usersCompanies)
        {
            Vehicle? vehicle = db.Vehicles.Find(vehicleId);

            if (vehicle == null) return false;
            if (!usersCompanies.Contains((int)vehicle.EnterpriseId!)) return false;
            try
            {
                db.Vehicles.Remove(vehicle);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
