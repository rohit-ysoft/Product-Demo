using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product.Models;
using Product.ViewModel;

namespace Product.Controllers
{
    public class ProductController : Controller
    {  
        private readonly productDbContext _product;
        public ProductController(productDbContext product)
        {
            _product = product;
        }
        public IActionResult Create()
        {
            var model = new productViewModel
            {
                ComplexityList = Enum.GetValues(typeof(Estimated_Complexity))
                                    .Cast<Estimated_Complexity>()
                                    .Select(e => new SelectListItem
                                    {
                                        Value = ((int)e).ToString(),
                                        Text = e.ToString()
                                    }),

                StatusList = Enum.GetValues(typeof(Status))
                                 .Cast<Status>()
                                 .Select(s => new SelectListItem
                                 {
                                     Value = ((int)s).ToString(),
                                     Text = s.ToString()
                                 })
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(productViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Convert the ViewModel to the Entity Model
                var product = new productModel
                {
                    Title = model.Title,
                    Description = model.Description,
                    Complexity = model.Complexity,
                    Status = model.Status,
                    TargetComplactionDate = model.TargetComplactionDate,
                    ActualComplactionDate = model.ActualComplactionDate
                };

                // Save the product entity to the database
                // ...

                return RedirectToAction("Index");
            }

            // Re-populate the dropdowns if model state is invalid
            model.ComplexityList = Enum.GetValues(typeof(Estimated_Complexity))
                                       .Cast<Estimated_Complexity>()
                                       .Select(e => new SelectListItem
                                       {
                                           Value = ((int)e).ToString(),
                                           Text = e.ToString()
                                       });

            model.StatusList = Enum.GetValues(typeof(Status))
                                   .Cast<Status>()
                                   .Select(s => new SelectListItem
                                   {
                                       Value = ((int)s).ToString(),
                                       Text = s.ToString()
                                   });

            return View(model);
        }



        public IActionResult Index()
        {
            var Products = _product.Products.ToList();
            return View(Products); 
        }

    }

}
