namespace Product.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }
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
