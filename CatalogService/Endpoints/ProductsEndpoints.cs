using Microsoft.AspNetCore.Mvc;
using ProductService.Dto.ProductDtos;
using ProductService.Interfaces;

namespace ProductService.Endpoints;

public static class ProductsEndpoints
{
    private static async Task<IResult> CreateProduct([FromBody]CreateProductDto dto, IProductService productService)
    {
        var result = await productService.CreateProduct(dto);
        return Results.Ok(result);
        
    }

    private static async Task<IResult> UpdateProduct([FromBody] UpdateProductDto dto, [FromRoute]Guid id, IProductService productService)
    {
        var result = await productService.UpdateProduct(id, dto);
        return result is null ? Results.NotFound() : Results.Ok(result); 
    }
    private static async Task<IResult> DeleteProduct([FromRoute]Guid guid, IProductService productService)
    {
        var result = await productService.DeleteProduct(guid);
        return result ? Results.NoContent() : Results.NotFound(result) ;
    }
    private static async Task<IResult> GetProduct([FromRoute]Guid guid, IProductService productService)
    {
        var result = await productService.GetProduct(guid);
        return result is null ? Results.NotFound() : Results.Ok(result);
    }

    private static async Task<IResult> GetProducts(IProductService productService)
    {
        var result = await productService.GetProducts();
        return Results.Ok(result);
    }

    private static void MapProduct(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/products/", GetProducts);
        builder.MapPost("/products/", CreateProduct);
        builder.MapPut("/products/{id:guid}", UpdateProduct);
        builder.MapDelete("/products/{id:guid}", DeleteProduct);
        builder.MapGet("/products/{guid:guid}", GetProduct);
    }
}