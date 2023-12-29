using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "manager")]
    public class EnterpriseController : BaseController.BaseController
    {
        private ApplicationDbContext _db;

        public EnterpriseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult<IEnumerable<Enterprise>> Index()
        {
            List<Enterprise> enterprises = _db
                .Enterprises
                .ToList();

            return enterprises;
        }
    }
}
