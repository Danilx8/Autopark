using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Autopark.Areas.Manager.Pages.Brands
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "manager")]
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Brand _brand { get; set; }
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                _brand = _db.Brands.First(u => u.Id == id);
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
            Brand? searchedBrand = _db.Brands.Find(_brand.Id);
            if (_brand == null)
            {
                return NotFound();
            }

            _db.Brands.Remove(searchedBrand);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }

}
