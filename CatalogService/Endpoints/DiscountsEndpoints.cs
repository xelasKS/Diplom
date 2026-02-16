using Microsoft.AspNetCore.Mvc;
using ProductService.Dto.DiscountDtos;
using ProductService.Interfaces;

namespace ProductService.Endpoints;

public static class DiscountsEndpoints
{
    private static async Task<IResult> CreateDiscount([FromBody]CreateDiscountDto dto, IDiscountService service) 
    {
        var result = await service.CreateDiscount(dto);
        return Results.Ok(result);    
    }

    private static async Task<IResult> DeleteDiscount([FromRoute] Guid discountId, IDiscountService service)
    {
        var result = await service.DeleteDiscount(discountId);
        return result ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UpdateDiscount([FromBody] UpdateDiscountDto dto, [FromRoute] Guid discountId, IDiscountService service)
    {
        var  result = await service.UpdateDiscount(dto, discountId);
        return result is null ? Results.NotFound() : Results.Ok(result);
    }

    private static async Task<IResult> GetDiscount([FromRoute] Guid discountId, 
        IDiscountService service)
    {
        var result = await service.GetDiscount(discountId);
        return result is null ? Results.NotFound() : Results.Ok(result);
    }

    public static void MapDiscounts(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/discounts/{id:guid}", GetDiscount);
        builder.MapPost("/discounts/", CreateDiscount);
        builder.MapPut("/discounts/{id:guid}", UpdateDiscount);
        builder.MapDelete("/discounts/{id:guid}", DeleteDiscount);
    } 
}