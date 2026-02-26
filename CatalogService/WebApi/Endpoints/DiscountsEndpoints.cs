using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Dto.DiscountDtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.WebApi.Endpoints;

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
        var group = builder.MapGroup("/discounts")
            .RequireAuthorization()
            .WithTags("Discounts");
        builder.MapGet("/{id:guid}", GetDiscount)
            .Produces<Discount>(StatusCodes.Status202Accepted)
            .Produces(StatusCodes.Status404NotFound);
        builder.MapPost("/", CreateDiscount)
            .Produces<Discount>(StatusCodes.Status202Accepted)
            .Produces(StatusCodes.Status404NotFound);
        builder.MapPatch("/{id:guid}", UpdateDiscount)
            .Produces<Discount>(StatusCodes.Status202Accepted)
            .Produces(StatusCodes.Status404NotFound);
        builder.MapDelete("/{id:guid}", DeleteDiscount)
            .Produces<Discount>(StatusCodes.Status202Accepted)
            .Produces(StatusCodes.Status404NotFound);;
    } 
}