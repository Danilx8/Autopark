using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Areas.Manager.Controllers
{
    public class EnterpriseController(ApplicationDbContext db) : BaseManagerController(db)
    {
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 300)]
        public ActionResult<IEnumerable<Enterprise>> Index()
        {
            List<Enterprise> enterprises = [.. _db
                .Enterprises];

            return enterprises;
        }
    }
}
