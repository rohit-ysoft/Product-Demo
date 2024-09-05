using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Product.Models;

namespace Product.Controllers
{
    public class ProductController : Controller
    {
        private readonly productDbContext _context;

        public ProductController(productDbContext context)
        {
            _context = context;
        }

        // Index action to display the list of products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            var viewModel = new productViewModel
            {
                Products = products,
                ComplexityList = GetSelectListItems<Complexity>(),
                StatusList = GetSelectListItems<Status>()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(productModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var viewModel = new productViewModel
            {
                Product = product,
                ComplexityList = GetSelectListItems<Complexity>(),
                StatusList = GetSelectListItems<Status>()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(productModel product, Complexity complexity)
        {

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var viewModel = new productViewModel
            {
                Product = product,
                ComplexityList = GetSelectListItems<Complexity>(),
                StatusList = GetSelectListItems<Status>()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public static List<SelectListItem> GetSelectListItems<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .Select(e => new SelectListItem
                       {
                           Value = ((int)(object)e).ToString(),
                           Text = e.ToString()
                       }).ToList();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
