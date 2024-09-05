using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Product.Models;

namespace Product.Controllers
{
    public class ProductController : Controller
    {
        private readonly productDbContext _context;
        private const int PageSize = 10;

        public ProductController(productDbContext context)
        {
            _context = context;
        }

        // Index action to display the list of products
        public async Task<IActionResult> Index(string searchQuery, int page = 1)
        {

            IQueryable<productModel> query = _context.Products;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p => p.Title.Contains(searchQuery));

            }


            var totalCount = await _context.Products.CountAsync();


            var products = await query
           .OrderBy(p => p.ID)
           .OrderBy(p => p.ID)
      .Skip((page - 1) * PageSize)
      .Take(PageSize)
        .ToListAsync();


            var model = new productViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = (int)System.Math.Ceiling((double)totalCount / PageSize),
                SearchQuery = searchQuery,
                ComplexityList = GetComplexityList(),
                StatusList = GetStatusList()
            };
            return View(model);
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
                ComplexityList = GetComplexityList(),
                StatusList = GetStatusList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(productModel product)
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
                ComplexityList = GetComplexityList(),
                StatusList = GetStatusList()
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

        // Helper methods to get lists for dropdowns
        private IEnumerable<SelectListItem> GetComplexityList()
        {
            return new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "S" },
            new SelectListItem { Value = "2", Text = "M" },
            new SelectListItem { Value = "3", Text = "L" },
            new SelectListItem { Value = "4", Text = "XL" }
        };
        }

        private IEnumerable<SelectListItem> GetStatusList()
        {
            return new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "New" },
            new SelectListItem { Value = "2", Text = "Active" },
            new SelectListItem { Value = "3", Text = "Closed" },
            new SelectListItem { Value = "4", Text = "Abandoned" }
        };
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
