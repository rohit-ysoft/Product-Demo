using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
    public class Estimated_Complexity
    {
        [Key]
        public int CId { get; set; }
        public string? Complexity { get; set; }    

    }
}
