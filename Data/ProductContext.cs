using apidotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace apidotnet.Data 
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) {}

        public DbSet<Product> Products {get; set;}
    }
}