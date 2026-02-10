using CatalogService.Models;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dto.DiscountDtos;

namespace ProductService.HostExtensions;

public static class DiscountEndpoints
{
    private static async Task<IResult> CreateDiscount([FromBody]CreateDiscountDto dto, ApplicationContext context) {
        var discount = new Discount(dto.Products, dto.Percent);
        await context.Discounts.AddAsync(discount);
        await context.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> DeleteDiscount([FromRoute] Guid discountId, ApplicationContext context)
    {
        var discount = await context.Discounts.FindAsync(discountId);
        if (discount is null)
        {
            return Results.NotFound();
        }

        context.Discounts.Remove(discount);
        await context.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> UpdateDiscount([FromBody] UpdateDiscountDto dto, ApplicationContext context, [FromRoute] Guid discountId)
    {
        var discount = await context.Discounts.FindAsync(discountId);
        discount.Products = dto.Products ?? discount.Products;
        discount.Percentage = dto.Percentage ?? discount.Percentage;
        await context.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> GetDiscount([FromRoute] Guid discountId, ApplicationContext context)
    {
        var discount = await context.Discounts.FindAsync(discountId);
        if (discount is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(discount);
    }

    private static void MapDiscount(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/discounts/{id:guid}", GetDiscount);
        builder.MapPost("/discounts/", CreateDiscount);
        builder.MapPut("/discounts/{id:guid}", UpdateDiscount);
        builder.MapDelete("/discounts/{id:guid}", DeleteDiscount);
    } 
}