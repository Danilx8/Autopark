using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Controllers
{
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
