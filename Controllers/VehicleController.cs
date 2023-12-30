using Autopark.Data;
using Autopark.Dto;
using Autopark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Autopark.Controllers
{
    public class VehicleController : BaseController.BaseController
    {
        private ApplicationDbContext _db;

        public VehicleController(ApplicationDbContext db)
        {
            _db = db;
        }

        [EnableCors("Frontend")]
        [HttpGet]
        public IActionResult Retrieve([FromQuery] PaginationFilter filter)
        {
            var vehicles = _db
                .Vehicles
                .Skip((filter.Page - 1) * filter.Limit)
                .Take(filter.Limit)
                .ToList();

            return Ok(vehicles);
        }

        public IActionResult Create(VehicleDto vehicle)
        {
            Brand? brand = _db
                .Brands
                .Where(b => b.Id == vehicle.BrandId)
                .FirstOrDefault();

            if (brand == null) return BadRequest();

            Enterprise? enterprise = _db
                .Enterprises
                .Where(e => e.Id == vehicle.EnterpriseId)
                .FirstOrDefault();

            if (enterprise == null) return BadRequest();

            Vehicle newVehicle = new()
            {
                Name            = vehicle.Name,
                Price           = vehicle.Price,
                ZeroToHundred   = vehicle.ZeroToHundred,
                Mileage         = vehicle.Mileage,
                Year            = vehicle.Year,
                HorsePower      = vehicle.HorsePower,
                BrandId         = vehicle.BrandId,
                EnterpriseId    = vehicle.EnterpriseId,
                DriverId        = vehicle.DriverId
            };

            _db.Vehicles.Add(newVehicle);
            _db.SaveChanges();

            return Ok(newVehicle.Id);
        }

        public IActionResult Update(VehicleDto vehicle)
        {
            Brand? brand = _db
               .Brands
               .Where(b => b.Id == vehicle.BrandId)
               .FirstOrDefault();

            if (brand == null) return BadRequest();

            Enterprise? enterprise = _db
                .Enterprises
                .Where(e => e.Id == vehicle.EnterpriseId)
                .FirstOrDefault();

            if (enterprise == null) return BadRequest();

            _db
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
            _db.SaveChanges();

            return Ok("Vehicle updated");
        }

        public IActionResult Delete(int id)
        {
            _db.Vehicles.Where(v => v.Id == id).ExecuteDelete();
            return Ok("Vehicle deleted");
        }
    }
}
