using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
        public async Task<IActionResult> Index(string searchQuery, int page = 1, string sortOrder = "asc")
        { 
            
            IQueryable<productModel> query = _context.Products;
            if (!string.IsNullOrEmpty(searchQuery))
            {  query = query.Where(p => p.Title.Contains(searchQuery));
            }

            // Apply sorting
            switch (sortOrder.ToLower())
            {
                case "desc":
                    query = query.OrderByDescending(p => p.Title);
                    break;
                case "asc":
                default:
                    query = query.OrderBy(p => p.Title);
                    break;
            }
            var totalCount = await _context.Products.CountAsync();
            var products = await query
           .Skip((page - 1) * PageSize)
           .Take(PageSize)
          .ToListAsync();
            var model = new productViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = (int)System.Math.Ceiling((double)totalCount / PageSize),
                SearchQuery = searchQuery,
                ComplexityList = GetSelectListItems<Complexity>(),
                StatusList = GetSelectListItems<Status>(),
                SortOrder = sortOrder
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
