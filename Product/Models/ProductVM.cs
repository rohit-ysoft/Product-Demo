using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
    public class ProductVM
    {
        [Key]
        public int ID { get; set; }
        [Required,StringLength(1000)]
        public string ?Title { get; set; }
        [Required,StringLength(5000)]
        public string ?Description { get; set; }
        public int CId { get; set; }
        public Estimated_Complexity ?Complexity { get; set; }
        public List<SelectListItem> ?ComplexityList { get; set; }
        public int SId { get; set; }
        public Status ?status { get; set; }
        public List<SelectListItem> ?StatusList { get; set; }
        public DateTime TargateComplactionDate { get; set; }
        public DateTime ActualComplactionDate { get; set; }
    }
}
