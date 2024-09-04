using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product.Models;

namespace Product.Controllers
{
    public class ProductController : Controller
    {

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
                StatusList = new SelectList(Enum.GetValues(typeof(Status)).Cast<Status>().Select(s => new
                {
                    Value = s,
                    Text = s.ToString()
                }), "Value", "Text"),

            };

           
            return View();
        }
    }
}
