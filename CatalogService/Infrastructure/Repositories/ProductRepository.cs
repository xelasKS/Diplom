using EFCore;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Dto.ProductDtos;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;

public class ProductRepository(ApplicationContext context) : IProductRepository
{
    public async Task DeleteProductAsync(Product product)
    {
        context.Products.Remove(product);
        await SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
         context.Update(product);
         await SaveChangesAsync();
    }

    public async Task CreateProductAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await context.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId)
    { 
        return await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == productId);
    }
    public async Task<Product?> GetProductByIdWithIncludeAsync(Guid productId) => 
        await context.Products
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == productId);
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}