using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Areas.Manager.Controllers
{
    public class BrandController(ApplicationDbContext db) : BaseManagerController(db)
    {
        public IActionResult Retrieve()
        {
            List<Brand> brands = [.. _db
                .Brands];

            return Ok(brands);
        }
    }
}
