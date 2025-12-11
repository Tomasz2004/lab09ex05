using Microsoft.EntityFrameworkCore;
using lab09webAPp.Models;

namespace lab09webAPp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "High-performance laptop",
                Price = 3999.99m,
                StockQuantity = 15,
                CreatedDate = DateTime.UtcNow
            },
            new Product
            {
                Id = 2,
                Name = "Mouse",
                Description = "Wireless gaming mouse",
                Price = 199.99m,
                StockQuantity = 50,
                CreatedDate = DateTime.UtcNow
            },
            new Product
            {
                Id = 3,
                Name = "Keyboard",
                Description = "Mechanical keyboard",
                Price = 399.99m,
                StockQuantity = 30,
                CreatedDate = DateTime.UtcNow
            }
        );
    }
}
