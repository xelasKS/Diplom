using ProductService.Application.Dto.ProductDtos;
using ProductService.Domain.Entities;

namespace ProductService.Domain.Interfaces;

public interface IProductRepository
{
    public Task DeleteProductAsync(Product product);
    public Task UpdateProductAsync(Product product);
    public Task CreateProductAsync(Product product);
    public Task<IEnumerable<Product>> GetAllProductsAsync();
    public Task<Product?> GetProductByIdAsync(Guid productId);
    public Task<Product?> GetProductByIdWithIncludeAsync(Guid productId);
    public Task SaveChangesAsync();
}