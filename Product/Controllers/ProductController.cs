using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product.Models;

namespace Product.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext _db;
        public ProductController(ProductDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()  
        {
            var model = new ProductVM
            {
                // Create the StatusList from the enum
                StatusList = _db.status.Select(c => new SelectListItem
                {
                    Value = c.SId.ToString(),
                    Text = c.status
                }).ToList(),
                // Create the ComplexityList from the fetched data
                ComplexityList = _db.complexity.Select(c => new SelectListItem
                {
                    Value = c.CId.ToString(),
                    Text = c.Complexity
                }).ToList()
            };

            return View(model);
        }
    }
}
