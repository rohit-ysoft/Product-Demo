using Microsoft.AspNetCore.Mvc.Rendering;
using Product.Models;
using System.ComponentModel.DataAnnotations;

namespace Product.ViewModel
{
    public class productViewModel
    {
        public int ID { get; set; }

        [Required, StringLength(1000)]
        public string Title { get; set; }

        [Required, StringLength(5000)]
        public string Description { get; set; }

        [Display(Name = "Estimated Complexity")]
        public Estimated_Complexity Complexity { get; set; }

        public IEnumerable<SelectListItem> ComplexityList { get; set; }

        public Status Status { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Target Completion Date")]
        [FutureDate(ErrorMessage = "Date should be in the future.")]
        public DateTime TargetComplactionDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Actual Completion Date")]
        [FutureDate(ErrorMessage = "Date should be in the future.")]
        public DateTime ActualComplactionDate { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (DateTime)value > DateTime.Now;
        }
    }
}
