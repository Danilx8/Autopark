using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Autopark.Areas.Manager.Pages.Vehicles
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "manager")]
    [BindProperties]
    public class DeleteModel : PageModel
    {
        public Vehicle _vehicle { get; set; }
        public List<Driver> _drivers { get; set; } = new();

        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                _vehicle = _db.Vehicles
                    .Include(u => u.Brand)
                    .Include(u => u.Enterprise)
                    .Include(u => u.Driver)
                    .Include(u => u.AssignedDrivers)
                    .First(u => u.Id == id);

                foreach (var driver in _vehicle.AssignedDrivers)
                {
                    _drivers.Add(driver);
                }
            }
            catch (ArgumentNullException err)
            {
                return NotFound(err);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            _db.Vehicles.Where(v => v.Id == _vehicle.Id).ExecuteDelete();
            _db.SaveChanges();
            return RedirectToPage("/Manager/Vehicles/Index/" + _vehicle.EnterpriseId);
        }
    }
}
