using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Dto.ProductDtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.WebApi.Endpoints;

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

    public static void MapProducts(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/products")
            .RequireAuthorization()
            .WithTags("Products");
        builder.MapGet("/", GetProducts)
            .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        builder.MapPost("/", CreateProduct)
            .Produces<Product>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound);
        builder.MapPut("/{id:guid}", UpdateProduct)
            .Produces<Product>(StatusCodes.Status202Accepted)
            .Produces(StatusCodes.Status404NotFound);
        builder.MapDelete("/{id:guid}", DeleteProduct)
            .Produces(StatusCodes.Status404NotFound);
        builder.MapGet("/{guid:guid}", GetProduct)
            .Produces<Product>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);;
    }
}