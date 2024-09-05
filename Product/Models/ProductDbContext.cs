using Microsoft.EntityFrameworkCore;

namespace Product.Models
{
    public class productDbContext:DbContext
    {
        public productDbContext(DbContextOptions<productDbContext> options)
         : base(options)
        {
        }

        public DbSet<productModel> Products { get; set; }
    }
}
