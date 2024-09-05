using Microsoft.AspNetCore.Mvc.Rendering;
using Product.Helper;
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
        public Complexity Complexity { get; set; }
        public Status Status { get; set; }
     
        [DataType(DataType.Date)]
        [Display(Name = "Target Completion Date")]
        [ConditionalRequired("Status", Status.Active, ErrorMessage = "Date should be in the future.")]
        public DateTime TargetCompletionDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Actual Completion Date")]
        
        [ConditionalRequired("Status", Status.Closed, ErrorMessage = "Date should be in the future.")]
        public DateTime ActualCompletionDate { get; set; }
    }
    public enum Complexity
    {
        S=1,
        M=2,
        L=3,
        XL=4

    }
    public enum Status
    {
        New=1,
        Active=2,
        Closed=3,
        Abandoned=4
    }
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (DateTime)value > DateTime.Now;
        }
    }
}