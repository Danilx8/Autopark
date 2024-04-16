using Autopark.Data;
using Autopark.Models;
using Autopark.Models.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Autopark.Services.Vehicles;

namespace Autopark.Areas.Manager.Controllers
{
    public class VehicleController(ApplicationDbContext db, IVehiclesService vehicleService) : BaseManagerController(db)
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
            if (enterpriseId == null)
            {
                int enterpriseIndex = 0;
                while(vehicles.Count < filter.Limit || enterpriseIndex < availableEnterprises.Count)
                {
                    vehicles.AddRange(vehicleService.GetAllVehicles(availableEnterprises[enterpriseIndex], filter));
                    ++enterpriseIndex;
                }

                return Ok(vehicles);
            }
            
            if (!availableEnterprises.Contains(enterpriseId ?? throw new Exception()))
                return BadRequest("You aren't authorized to manage this enterprise");
            
            return Ok(vehicleService.GetAllVehicles(enterpriseId ?? throw new Exception(), filter));
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
