using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dto.CategoryDtos;
using ProductService.Interfaces;

namespace ProductService.Endpoints;

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
        builder.MapGet("/categories/{categoryId:guid}", GetCategory);
        builder.MapPost("/categories", CreateCategory);
        builder.MapPut("/categories/{categoryId:guid}", UpdateCategory);
        builder.MapDelete("/categories/{categoryId:guid}", DeleteCategory);
    }
}