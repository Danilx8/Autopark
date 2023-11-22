using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Autopark.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _db;
        public List<Vehicle> Vehicles { get; set; } = null!;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public void OnGet()
        {
            Vehicles = _db.Vehicles
                .Include(u => u.Brand)
                .ToList();
        }
    }
}
