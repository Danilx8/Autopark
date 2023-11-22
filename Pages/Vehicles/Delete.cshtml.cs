using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Autopark.Pages
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        public Vehicle _vehicle { get; set; }

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
                    .First(u => u.Id == id);
            } catch (ArgumentNullException err)
            {
                return NotFound(err);
            } catch (Exception e)
            {
                return BadRequest(e);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            Vehicle? searchedVehicle = _db.Vehicles.Find(_vehicle.Id);
            if (_vehicle == null)
            {
                return NotFound();
            }

            _db.Vehicles.Remove(searchedVehicle);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
