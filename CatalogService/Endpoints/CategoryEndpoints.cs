using System.Runtime.CompilerServices;
using CatalogService.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dto;
using ProductService.Dto.CategoryDtos;

namespace ProductService.HostExtensions;

public static class CategoryEndpoints
{
    private static async Task<IResult> UpdateCategory(UpdateCategoryDto dto, Guid categoryId, ApplicationContext context)
    {
        var category = await context.Categories.FindAsync(categoryId);
        if (category is null)
        {
            return Results.NotFound();
        }
        category.Name = dto.Name ??  category.Name;
        category.Description = dto.Description ?? category.Description;
        await context.SaveChangesAsync();
        return Results.Ok(category);
            
    }

    private static async Task<IResult> DeleteCategory([FromRoute]Guid categoryId, ApplicationContext context)
    {
        var category = await context.Categories.FindAsync(categoryId);
        if (category is null)
        {
            return Results.NotFound();
        }
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> CreateCategory([FromBody]CreateCategoryDto dto, ApplicationContext context)
    {
       var cat = new Category(dto.Name, dto.Description);
       await context.Categories.AddAsync(cat);
       return Results.Ok();
    }
    private static async Task<IResult> GetCategory([FromRoute]Guid categoryId, ApplicationContext context)
    {
        var category = await context.Categories.FindAsync(categoryId);
        if (category is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(category);
    }

    public static void MapCategory(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/categories/{categoryId:guid}", GetCategory);
        builder.MapPost("/categories", CreateCategory);
        builder.MapPut("/categories/{categoryId:guid}", UpdateCategory);
    }
}