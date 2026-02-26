using ProductService.Application.Dto.ProductDtos;
using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface IProductService
{
    public Task<bool> DeleteProduct(Guid productId);
    public Task<Product?> UpdateProduct(Guid productId, UpdateProductDto dto);
    public Task<Product> CreateProduct(CreateProductDto dto);
    public Task<IEnumerable<Product>> GetProducts();
    public Task<Product?> GetProduct(Guid productId);
}