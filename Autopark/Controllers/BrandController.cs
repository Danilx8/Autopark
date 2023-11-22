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

        public ActionResult<IEnumerable<Brand>> Index()
        {
            List<Brand> brands = _db
                .Brands
                .ToList();

            return brands;
        } 
    }
}
