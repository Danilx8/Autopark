using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Controllers
{
    public class BrandController : BaseController.BaseController
    {
        private ApplicationDbContext _db;

        public BrandController(ApplicationDbContext db)
        {
            _db = db;    
        }

        public IActionResult Retrieve()
        {
            List<Brand> brands = _db
                .Brands
                .ToList();

            return Ok(brands);
        } 
    }
}
