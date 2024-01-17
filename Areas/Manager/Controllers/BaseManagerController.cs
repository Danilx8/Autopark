using Autopark.Controllers.BaseController;
using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Autopark.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "manager")]
    public abstract class BaseManagerController : BaseController
    {
        protected ApplicationDbContext _db;

        public BaseManagerController(ApplicationDbContext db)
        {
            _db = db;
        }

        protected List<int> AuthorizeUsersEnterprises()
        {
            var idClaim = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

            if (idClaim == null) throw new UnauthorizedAccessException();

            AppUser? currentUser = _db.Users
                .Include(u => u.ManagedCompanies)
                .Where(u => u.Id == idClaim.Value)
                .FirstOrDefault();

            if (currentUser == null) throw new UnauthorizedAccessException();
            if (currentUser.ManagedCompanies == null) throw new UnauthorizedAccessException();

            List<int> usersCompanies = new();
            foreach (var company in currentUser.ManagedCompanies)
            {
                usersCompanies.Add(company.ManagedEnterpriseId);
            }

            return usersCompanies;
        }
    }
}
