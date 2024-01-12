using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Autopark.Pages.Enterprises
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "manager")]
    public class IndexModel(ApplicationDbContext db) : PageModel
    {
        private ApplicationDbContext _db = db;

        public List<Enterprise> Enterprises { get; set; } = [];
        public void OnGet()
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (idClaim == null) return;

            AppUser? currentUser = _db.Users
                .Include(u => u.ManagedCompanies)
                .Where(u => u.Id == idClaim.Value)
                .FirstOrDefault();

            foreach (EnterpriseManager enterprise in currentUser!.ManagedCompanies!)
            {
                Enterprise? current = _db
                    .Enterprises
                    .Where(e => e.Id == enterprise.ManagedEnterpriseId)
                    .FirstOrDefault();

                if (current != null) Enterprises.Add(current);
            }
        }
    }
}
