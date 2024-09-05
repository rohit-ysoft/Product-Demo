using Microsoft.AspNetCore.Mvc.Rendering;

namespace Product.Models
{
    public class productViewModel
    {
        public List<productModel> Products { get; set; }
        public productModel Product { get; set; }
        public IEnumerable<SelectListItem> ComplexityList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SearchQuery { get; set; }
    }

}

