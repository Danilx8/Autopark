using Autopark.Data;
using Autopark.Dto;
using Autopark.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Controllers
{
    public class DriverController : BaseController.BaseController
    {
        private ApplicationDbContext _db;

        public DriverController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult<IEnumerable<DriverDto>> Index()
        {
            List<DriverDto> drivers = _db
                .Drivers
                .Select(d => new DriverDto
                {
                    Id              = d.Id,
                    Name            = d.Name,
                    Salary          = d.Salary,
                    EnterpriseId    = d.EnterpriseId,
                    VehicleId       = d.Vehicle == null ? 0 : d.Vehicle.Id,
                    AssignedCarsId  = d.AssignedCars.Select(c => c.Id)
                })
                .ToList();

            return drivers;
        }
    }
}
