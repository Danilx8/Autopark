﻿using Autopark.Controllers.BaseController;
using Autopark.Data;
using Autopark.Dto;
using Autopark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Autopark.Controllers
{
    public class VehicleController : BaseManagerController
    {
        public VehicleController(ApplicationDbContext db) : base(db)
        {
        }

        public IActionResult Retrieve()
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

            var vehicles = _db
                .Vehicles
                .Where(v => usersCompanies.Contains((int)v.EnterpriseId))
                .Select(v => new VehicleDto(v))
                .ToList();

            return Ok(vehicles);
        }

        public IActionResult Create(VehicleDto vehicle)
        {
            Brand? brand = _db
                .Brands
                .Where(b => b.Id == vehicle.BrandId)
                .FirstOrDefault();

            if (brand == null) return BadRequest("Given brand doesn't exist");

            Enterprise? enterprise = _db
                .Enterprises
                .Where(e => e.Id == vehicle.EnterpriseId)
                .FirstOrDefault();

            if (enterprise == null) return BadRequest("Given enterprise doesn't exist");

            List<int> usersCompanies = new();
            try
            {
                usersCompanies = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            if (!usersCompanies.Contains(enterprise.Id)) return StatusCode(StatusCodes.Status403Forbidden);

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

            if (brand == null) return BadRequest("Given brand doesn't exist");

            Enterprise? enterprise = _db
                .Enterprises
                .Where(e => e.Id == vehicle.EnterpriseId)
                .FirstOrDefault();

            if (enterprise == null) return BadRequest("Given enterprise doesn't exist");

            List<int> usersCompanies = new();
            try
            {
                usersCompanies = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }

            if (!usersCompanies.Contains(enterprise.Id)) return StatusCode(StatusCodes.Status403Forbidden);

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
            List<int> usersCompanies = new();
            try
            {
                usersCompanies = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }

            Vehicle? vehicle = _db.Vehicles.Find(id);

            if (vehicle == null) return BadRequest("Given vehicle doesn't exist");
            if (!usersCompanies.Contains((int)vehicle.EnterpriseId)) return StatusCode(StatusCodes.Status403Forbidden);

            _db.Vehicles.Remove(vehicle);
            _db.SaveChanges();
            return Ok("Vehicle deleted");
        }
    }
}
