using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autopark.Pages.Brands
{
    public class UpsertModel : PageModel
    {
        private readonly ILogger<UpsertModel> _logger;
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Brand _brand { get; set; }
        public IEnumerable<SelectListItem> _types { get; set; }
        public IEnumerable<SelectListItem> _segments { get; set; }
        public IEnumerable<SelectListItem> _categories { get; set; }

        public UpsertModel(ILogger<UpsertModel> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult OnGet(int? id)
        {
            _types = Enum.GetValues(typeof(Brand.Types)).Cast<Brand.Types>().Select(t => new SelectListItem
            {
                Text = t.ToString().ToLower(),
                Value = t.ToString()
            });
            _segments = Enum.GetValues(typeof(Brand.Segments)).Cast<Brand.Segments>().Select(s => new SelectListItem
            {
                Text = s.ToString().ToLower(),
                Value = s.ToString()
            });
            _categories = Enum.GetValues(typeof(Brand.Categories)).Cast<Brand.Categories>().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            });

            if (id == null)
            {
                return Page();
            }

            _brand = _db.Brands.FirstOrDefault(u => u.Id == id);
            if (_brand == null)
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

            if (_brand.Id == 0)
            {
                _db.Add(_brand);
            }
            else
            {
                _db.Update(_brand);
            }

            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
