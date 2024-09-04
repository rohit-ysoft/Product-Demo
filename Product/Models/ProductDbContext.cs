using Microsoft.EntityFrameworkCore;

namespace Product.Models
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext>options):base (options) { }
        public DbSet<Product> product { get; set; }
        public DbSet<Estimated_Complexity> complexity { get; set; }
    }
}
