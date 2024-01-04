using Autopark.Controllers.BaseController;
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
    public class DriverController : BaseManagerController
    {
        public DriverController(ApplicationDbContext db) : base(db)
        {
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
            List<int> usersCompanies = new();
            try
            {
                usersCompanies = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }

            if (!usersCompanies.Contains(driver.EnterpriseId)) return Forbid("You do not manage given enterprise");

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

            _db.SaveChanges();

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
            List<int> usersCompanies = new();
            try
            {
                usersCompanies = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }

            if (!usersCompanies.Contains(driver.EnterpriseId)) return Forbid("You do not manage given enterprise");

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

            if (vehicle?.EnterpriseId != driver.EnterpriseId) return BadRequest("Vehicle enterprise and driver enterprise don't match up");

            Driver? oldDriver = _db
                .Drivers
                .Where(d => d.Id == driver.Id)
                .Include(d => d.Vehicle)
                .FirstOrDefault();

            if (oldDriver != null && oldDriver.Vehicle != null)
            {
                oldDriver.Vehicle= null;
            }

            _db
                .Drivers
                .Where(d => d.Id == driver.Id)
                .ExecuteUpdate(s => s
                    .SetProperty(d => d.Name, d => driver.Name)
                    .SetProperty(d => d.Salary, d => driver.Salary)
                    .SetProperty(d => d.EnterpriseId, d => driver.EnterpriseId));

            _db.SaveChanges();

            if (vehicle != null)
            {
                vehicle.DriverId = driver.Id;
                _db.Vehicles.Update(vehicle);
            }

            _db.SaveChanges();

            return Ok("Driver updated");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            List<int> usersCompanies = new();
            try
            {
                usersCompanies = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }

            Driver? driver = _db
                .Drivers
                .Where(d => d.Id == id)
                .FirstOrDefault();

            if (driver == null) return NotFound("Given driver doesn't exist");

            if (!usersCompanies.Contains(driver.EnterpriseId)) return Forbid("You do not manage given enterprise");

            _db.Drivers.Remove(driver);
            _db.SaveChanges();
            return Ok("Driver deleted");
        }
    }
}
