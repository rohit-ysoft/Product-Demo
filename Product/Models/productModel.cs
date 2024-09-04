using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
    public class productModel
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(1000)]
        public string Title { get; set; }

        [Required, StringLength(5000)]
        public string Description { get; set; }

        public Estimated_Complexity Complexity { get; set; }

        public Status Status { get; set; }

        public DateTime TargetComplactionDate { get; set; }

        public DateTime ActualComplactionDate { get; set; }
    }

    public enum Estimated_Complexity
    {
        S = 0,
        M = 1,
        L = 2,
        XL = 3
    }

    public enum Status
    {
        New = 0,
        Active = 1,
        Closed = 2,
        Abandoned = 3
    }
}
