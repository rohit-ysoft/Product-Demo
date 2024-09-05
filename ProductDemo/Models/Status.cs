using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
    public class Status
    {
        [Key]
        public int SId { get; set; }
        public string? status { get; set; }
    }
}
