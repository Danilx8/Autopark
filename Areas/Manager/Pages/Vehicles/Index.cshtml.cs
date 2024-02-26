using Autopark.Data;
using Autopark.Migrations;
using Autopark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Autopark.Areas.Manager.Pages.Vehicles
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "manager")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _db;
        public List<Vehicle> Vehicles { get; set; } = null!;
        public string EnterpriseName { get; set; } = "No enterprise chosen";
        public int EnterpriseId { get; set; }
        public int PageNumber { get; set; } = 1;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public void OnGet(int enterpriseId, [FromQuery] PaginationFilter filter)
        {
            Vehicles = [..
                _db
                .Vehicles
                .Where(e => e.EnterpriseId == enterpriseId)
                .Skip((filter.PageNum - 1) * filter.Limit)
                .Take(filter.Limit)
                .Include(u => u.Brand)];

            string? name = _db.Enterprises.Where(e => e.Id == enterpriseId).FirstOrDefault()?.Name;
            if (name != null) EnterpriseName = name;
            EnterpriseId = enterpriseId;
            PageNumber = filter.PageNum;
        }
    }
}
