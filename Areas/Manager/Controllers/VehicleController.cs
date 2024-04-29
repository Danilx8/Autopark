using Autopark.Data;
using Autopark.Models;
using Autopark.Models.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Autopark.Services.Vehicles;

namespace Autopark.Areas.Manager.Controllers
{
    public class VehicleController(ApplicationDbContext db, IVehiclesService vehicleService, 
        ILogger<VehicleController> logger) : BaseManagerController(db)
    {
        [EnableCors("Frontend")]
        [HttpGet]
        public IActionResult Retrieve([FromQuery] PaginationFilter filter, int? enterpriseId)
        {
            List<int> availableEnterprises;
            try
            {
                availableEnterprises = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            
            List<Vehicle> vehicles = [];
            var limit = filter.Limit;
            if (enterpriseId == null)
            {
                int enterpriseIndex = 0;
                while(vehicles.Count < filter.Limit && enterpriseIndex < availableEnterprises.Count)
                {
                    vehicles.AddRange(vehicleService.GetAllVehicles(availableEnterprises[enterpriseIndex], filter));
                    ++enterpriseIndex;
                }

                if (vehicles.Count > filter.Limit)
                {
                    var limitedSegment = new ArraySegment<Vehicle>(vehicles.ToArray());
                    vehicles = limitedSegment[..(limit - 1)].ToList();
                }
                
                var number = vehicles.Count;
                logger.LogInformation("Requested all vehicles without specifying an enterprise. Returned " +
                                       "{Number} vehicles of {Limit} limit requested.", number, limit);
                return Ok(vehicles);
            }
            
            if (!availableEnterprises.Contains(enterpriseId ?? throw new Exception()))
                return BadRequest("You aren't authorized to manage this enterprise");

            vehicles = vehicleService.GetAllVehicles(enterpriseId ?? throw new Exception(), filter);
            var count = vehicles.Count;
            logger.LogInformation("Returned {Count} vehicles of requested {Limit} limit of the enterprise " +
                                   "with id of {EnterpriseId}", count, limit, enterpriseId);
            return Ok(vehicles);
        }
        
        [EnableCors("Frontend")]
        [HttpPost]
        public IActionResult FindByName([FromForm] string name)
        {
            List<int> availableEnterprises;
            try
            {
                availableEnterprises = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }

            var vehicle = vehicleService.FindVehicleByName(name);
            if (vehicle == null) return NoContent();
            if (!availableEnterprises.Contains(vehicle.EnterpriseId ?? throw new Exception()))
            {
                return Forbid();
            }

            logger.LogInformation("By given name {Name} the following vehicle was found: {@Vehicle}", name, vehicle);
            return Ok(vehicle);
        }

        public IActionResult Create(VehicleDto vehicle)
        {
            List<int> enterprises;
            try
            {
                enterprises = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            int? id = vehicleService.CreateVehicle(vehicle, enterprises);
            if (id == null) return BadRequest("Couldn't create vehicle");
            return Ok(id);
        }

        public IActionResult Update(VehicleDto vehicle)
        {
            List<int> enterprises;
            try
            {
                enterprises = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            if (vehicleService.Update(vehicle, enterprises)) return Ok("Vehicle updated");
            return BadRequest("Couldn't update the vehicle");
        }

        public IActionResult Delete(int id)
        {
            List<int> usersCompanies;
            try
            {
                usersCompanies = AuthorizeUsersEnterprises();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            if (vehicleService.DeleteVehicle(id, usersCompanies)) return Ok("Vehicle deleted");
            return BadRequest("Couldn't delete the vehicle");
        }
    }
}
