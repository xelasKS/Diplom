using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductService;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    private readonly IConfiguration _configuration;
    public ApplicationContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }
}