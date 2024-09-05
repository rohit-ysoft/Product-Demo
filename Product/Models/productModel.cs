using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
    public class productModel
    {
        [Key]
        public int ID { get; set; }
        [Required,StringLength(1000)]
        public string Title { get; set; }
        [Required, StringLength(5000)]
        public string Description { get; set; }
        public int Complexity { get; set; }
        public int Status { get; set; }
     
        [DataType(DataType.Date)]
        [Display(Name = "Target Completion Date")]
        [FutureDate(ErrorMessage = "Date should be in the future.")]
        public DateTime TargetCompletionDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Actual Completion Date")]
        [FutureDate(ErrorMessage = "Date should be in the future.")]
        public DateTime ActualCompletionDate { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (DateTime)value > DateTime.Now;
        }
    }
}