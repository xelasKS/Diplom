using ProductService.Application.Dto.CategoryDtos;
using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface ICategoryService
{
    public Task<Category?> UpdateCategory(UpdateCategoryDto dto, Guid categoryId);
    public Task<bool> DeleteCategory(Guid categoryId);
    public Task<Category> CreateCategory(CreateCategoryDto dto);
    public Task<Category?> GetCategory(Guid categoryId);
}