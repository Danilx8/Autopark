using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Autopark.Areas.Manager.Pages.Vehicles
{

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "manager")]
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Vehicle _vehicle { get; set; }
        public Enterprise _enterprise { get; set; }
        public IEnumerable<SelectListItem> _brands { get; set; }
        public IEnumerable<SelectListItem>? _drivers { get; set; }

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int? id, int? enterpriseId)
        {
            _brands = _db.Brands.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            _enterprise = _db
                .Enterprises
                .Where(e => e.Id == enterpriseId)
                .First();

            _drivers = _db.Drivers
                .Where(d => d.EnterpriseId == _enterprise.Id)
                .ToList()
                .Select(u => new SelectListItem
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
                .FirstOrDefault(u => u.Id == id)!;

            if (_vehicle == null)
            {
                return NotFound();
            }

            if (_vehicle.EnterpriseId != enterpriseId)
            {
                _enterprise = _db
                    .Enterprises
                    .Where(e => e.Id == _vehicle.EnterpriseId)
                    .First();

                _drivers = _db.Drivers
                    .Where(d => d.EnterpriseId == _enterprise.Id)
                    .ToList()
                    .Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });
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
            }
            else
            {
                _db.Update(_vehicle);
            }

            _db.SaveChanges();
            return Redirect("/Manager/Vehicles/Index/" + _vehicle.EnterpriseId);
        }
    }
}
