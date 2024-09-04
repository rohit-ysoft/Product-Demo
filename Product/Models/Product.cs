using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required,StringLength(1000)]
        public string Title { get; set; }
        [Required,StringLength(5000)]
        public string Description { get; set; }
        public Estimated_Complexity Complexity { get; set; }
        public Status status { get; set; }
        public DateTime TargateComplactionDate { get; set; }
        public DateTime ActualComplactionDate { get; set; }
    }
    public enum Status
    {
        New = 0,
        Active = 1,
        Closed = 2,
        Abandoned = 3,
    }
}
