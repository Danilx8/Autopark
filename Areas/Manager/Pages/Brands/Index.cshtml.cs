using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Autopark.Areas.Manager.Pages.Brands
{

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "manager")]
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
