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
                status = Status.New, // Default value, if any
                // to retrive enom list for dropdown
                StatusList = new SelectList(Enum.GetValues(typeof(Status)).Cast<Status>().Select(s => new
                {
                    Value = s,
                    Text = s.ToString()
                }), "Value", "Text"),
                // to retrive Estimated_Complexity list from database for dropdown
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
