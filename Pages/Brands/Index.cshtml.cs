using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Autopark.Pages.Brands
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _db;
        public List<Brand> Brands { get; set; } = null!;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            Brands = _db.Brands.ToList();
        }
    }
}
