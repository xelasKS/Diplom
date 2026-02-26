using ProductService.Application.Dto.CategoryDtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Services;

public class CategoryService(ApplicationContext context) : ICategoryService
{
    public async Task<Category?> UpdateCategory(UpdateCategoryDto dto, Guid categoryId)
    {
        var category = await context.Categories.FindAsync(categoryId);
        if (category is null)
        {
            return null;
        }
        category.Name = dto.Name ??  category.Name;
        category.Description = dto.Description ?? category.Description;
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> DeleteCategory(Guid categoryId)
    {
        var category = await context.Categories.FindAsync(categoryId);
        if (category is null)
        {
            return false;
        }
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Category> CreateCategory(CreateCategoryDto dto)
    {
        var category = new Category(dto.Name, dto.Description);
        await context.Categories.AddAsync(category);
        return category;
    }

    public async Task<Category?> GetCategory(Guid categoryId)
    {
        var category = await context.Categories.FindAsync(categoryId);
        if (category is null)
        {
            return null;
        }
        return category;
    }
}