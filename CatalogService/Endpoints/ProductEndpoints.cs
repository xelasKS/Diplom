using CatalogService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Dto;
using ProductService.Dto.ProductDtos;

namespace ProductService.HostExtensions;

public static class ProductEndpoints
{
    private static async Task<IResult> CreateProduct([FromBody]CreateProductDto createProduct, ApplicationContext context)
    {
        var product = new Product(createProduct.Name, createProduct.Description, createProduct.Price, createProduct.Images, createProduct.Category);
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return Results.Ok(product.Id);
    }

    private static async Task<IResult> UpdateProduct([FromBody] UpdateProductDto updateProduct,
        ApplicationContext context, [FromRoute]Guid id)
    {
        var product = await context.Products.Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
        {
            return Results.NotFound();
        }
        product.Name = updateProduct.Name ?? product.Name;
        product.Description = updateProduct.Description ?? product.Description;
        product.Images =  updateProduct.Images ?? product.Images;
        product.Price = updateProduct.Price ?? product.Price;
        product.Category = updateProduct.Category ?? product.Category;
        await context.SaveChangesAsync();
        return Results.Ok(product.Id);
    }
    private static async Task<IResult> DeleteProduct(ApplicationContext applicationContext, [FromRoute]Guid guid)
    {
        applicationContext.Products.Remove((await applicationContext.Products.FindAsync(guid))!);
        return Results.NoContent();
    }
    private static async Task<IResult> GetProduct(ApplicationContext applicationContext, Guid guid)
    {
        var product = await applicationContext.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(x => x.Id == guid);
        if (product is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(product);
    }

    private static async Task<IResult> GetProducts([FromRoute] Guid categoryId, ApplicationContext applicationContext)
    {
        var products = applicationContext.Products
            .Include(p => p.Category)
            .Where(p => p.Category.Id == categoryId);
        if (!await products.AnyAsync())
        {
            return Results.NotFound();
        }
        return Results.Ok(products);
    }

    private static void MapProduct(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/categories/{categoryId:guid}/products", GetProducts);
        builder.MapPost("/products/", CreateProduct);
        builder.MapPut("/products/{id:guid}", UpdateProduct);
        builder.MapDelete("/products/{id:guid}", DeleteProduct);
        builder.MapGet("/products/{guid:guid}", GetProduct);
    }
}