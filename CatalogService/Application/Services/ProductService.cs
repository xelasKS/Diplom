using ProductService.Application.Dto.ProductDtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<bool> DeleteProduct(Guid productId)
    {
        var product = await productRepository.GetProductByIdAsync(productId);
        if (product is null)
        {
            return false;
        }
        await productRepository.DeleteProductAsync(product);
        return true;
    }

    public async Task<Product?> UpdateProduct(Guid productId, UpdateProductDto dto)
    {
        var product = await productRepository.GetProductByIdWithIncludeAsync(productId);
        if (product == null)
        {
            return null;
        }
        product.UpdateProduct(dto);
        await productRepository.UpdateProductAsync(product);
        return product;
    }

    public async Task<Product> CreateProduct(CreateProductDto dto)
    {
        var product = new Product(dto.Name, dto.Description, dto.Price, dto.Images, dto.Category);
        await productRepository.CreateProductAsync(product);
        return product;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await productRepository.GetAllProductsAsync();
    }

    public async Task<Product?> GetProduct(Guid productId)
    {
        return await productRepository.GetProductByIdAsync(productId);
    }
}