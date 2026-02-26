using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Dto.CategoryDtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.WebApi.Endpoints;

public static class CategoriesEndpoints
{
    private static async Task<IResult> UpdateCategory([FromBody]UpdateCategoryDto dto, [FromRoute]Guid categoryId, ICategoryService categoryService)
    {
        var result =  await categoryService.UpdateCategory(dto, categoryId);
        return Results.Ok(result);
    }

    private static async Task<IResult> DeleteCategory([FromRoute]Guid categoryId,
        ICategoryService categoryService)
    {
        await categoryService.DeleteCategory(categoryId);
        return Results.NoContent();
    }

    private static async Task<IResult> CreateCategory([FromBody]CreateCategoryDto dto,
        ICategoryService categoryService)
    {
        var result = await categoryService.CreateCategory(dto); 
       return Results.Ok(result);
    }
    private static async Task<IResult> GetCategory([FromRoute]Guid categoryId,
        ICategoryService categoryService)
    {
        var result = await categoryService.GetCategory(categoryId);
        return Results.Ok(result);
    }

    public static void MapCategories(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/categories")
            .RequireAuthorization()
            .WithTags("Categories"); // Группировка в Swagger

        group.MapGet("/{categoryId:guid}", GetCategory)
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", CreateCategory)
            .Produces<Category>(StatusCodes.Status201Created);

        group.MapPatch("/{categoryId:guid}", UpdateCategory)
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{categoryId:guid}", DeleteCategory)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}