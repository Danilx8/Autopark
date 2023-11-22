using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Autopark.Pages.Vehicles
{
    public class UpsertModel : PageModel
    {
        private readonly ILogger<UpsertModel> _logger;
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Vehicle _vehicle { get; set; }
        public IEnumerable<SelectListItem> _brands { get; set; }

        public UpsertModel(ILogger<UpsertModel> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult OnGet(int? id)
        {
            _brands = _db.Brands.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            if (id == null)
            {
                return Page();
            }

            _vehicle = _db.Vehicles
                .Include(u => u.Brand)
                .FirstOrDefault(u => u.Id == id);
            if (_vehicle == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_vehicle.Id == 0)
            {
                _db.Add(_vehicle);
            } else
            {
                _db.Update(_vehicle);
            }

            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
