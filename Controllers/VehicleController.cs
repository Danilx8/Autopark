using Autopark.Data;
using Autopark.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Controllers
{
    public class VehicleController : BaseController.BaseController
    {
        private ApplicationDbContext _db;

        public VehicleController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult<IEnumerable<VehicleDto>> Index()
        {
            var vehicles = _db
                .Vehicles
                .Select(v => new VehicleDto {
                    Id              = v.Id,
                    Name            = v.Name,
                    Price           = v.Price,
                    ZeroToHundred   = v.ZeroToHundred,
                    Mileage         = v.Mileage,
                    Year            = v.Year,
                    HorsePower      = v.HorsePower,
                    BrandId         = v.BrandId,
                    DriverId        = v.DriverId,
                    AssignedDrivers = v.AssignedDrivers
                })
                .ToList();

            return vehicles;
        }
    }
}
