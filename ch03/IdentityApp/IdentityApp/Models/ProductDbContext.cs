using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models
{
    public class ProductDbContext : DbContext 
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) 
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                    new Product { Name = "Kayak", Category = "Watersports", Price = 275 },
                    new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95m },
                    new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50m },
                    new Product { Name = "Corner flags", Category = "Soccer", Price = 34.95m },
                    new Product { Name = "Stadium", Category = "Soccer", Price = 79500 },
                    new Product { Name = "Thinking Cap", Category = "Chess", Price = 16 },
                    new Product { Name = "Unsteady Chair", Category = "Chess", Price = 29.95m },
                    new Product { Name = "Human Chess Board", Category = "Chess", Price = 75 },
                    new Product { Name = "Bling-Bling King", Category = "Chess", Price = 1200 }
                );
        }
    }
}
