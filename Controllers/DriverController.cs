using Autopark.Data;
using Autopark.Dto;
using Autopark.Models;
using Autopark.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Autopark.Controllers
{
    public class DriverController : BaseController.BaseController
    {
        private ApplicationDbContext _db;

        public DriverController(ApplicationDbContext db)
        {
            _db = db;
        }

        [EnableCors("Frontend")]
        [HttpGet]
        public IActionResult Retrieve([FromQuery] PaginationFilter filter)
        {
            List<DriverDto> drivers = _db
                .Drivers
                .Skip((filter.Page - 1) * filter.Limit)
                .Take(filter.Limit)
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

            return Ok(drivers);
        }

        public IActionResult Create(DriverDto driver)
        {
            var idClaim = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

            if (idClaim == null) return Unauthorized();

            AppUser? currentUser = _db.Users
                .Include(u => u.ManagedCompanies)
                .Where(u => u.Id == idClaim.Value)
                .FirstOrDefault();

            if (currentUser == null) return Unauthorized();
            if (currentUser.ManagedCompanies == null) return Ok();

            List<int> usersCompanies = new();
            foreach (var company in currentUser.ManagedCompanies)
            {
                usersCompanies.Add(company.ManagedEnterpriseId);
            }

            if (!usersCompanies.Contains(driver.EnterpriseId)) return BadRequest("You do not manage given enterprise");

            Enterprise? enterprise = _db
                .Enterprises
                .Where(e => e.Id == driver.EnterpriseId)
                .FirstOrDefault();

            if (enterprise == null) return BadRequest("Given enterprise does not exist");

            Vehicle? vehicle = _db
                .Vehicles
                .Where(v => v.Id == driver.VehicleId)
                .FirstOrDefault();

            if (vehicle == null && driver.VehicleId != 0) return BadRequest("Given vehicle does not exist");

            Driver newDriver = new()
            {
                Name = driver.Name,
                Salary = driver.Salary,
                EnterpriseId = driver.EnterpriseId
            };

            _db.Drivers.Add(newDriver);

            if (vehicle != null)
            {
                vehicle.DriverId = newDriver.Id;
                _db.Vehicles.Update(vehicle);
            }

            _db.SaveChanges();

            return Ok(newDriver.Id);
        }

        public IActionResult Update(DriverDto driver)
        {
            Enterprise? enterprise = _db
                .Enterprises
                .Where(e => e.Id == driver.EnterpriseId)
                .FirstOrDefault();

            if (enterprise == null) return BadRequest("Given enterprise does not exist");

            Vehicle? vehicle = _db
                .Vehicles
                .Where(v => v.Id == driver.VehicleId)
                .FirstOrDefault();

            if (vehicle == null && driver.VehicleId != 0) return BadRequest("Given vehicle does not exist");

            _db
                .Drivers
                .Where(d => d.Id == driver.Id)
                .ExecuteUpdate(s => s
                    .SetProperty(d => d.Name, d => driver.Name)
                    .SetProperty(d => d.Salary, d => driver.Salary)
                    .SetProperty(d => d.EnterpriseId, d => driver.EnterpriseId));

            if (vehicle != null)
            {
                vehicle.DriverId = driver.Id;
                _db.Vehicles.Update(vehicle);
            }

            _db.SaveChanges();

            return Ok("Driver updated");
        }

        public IActionResult Delete(DriverDto driver)
        {
            _db.Drivers.Where(d => d.Id == driver.Id).ExecuteDelete();
            return Ok("Driver deleted");
        }
    }
}
