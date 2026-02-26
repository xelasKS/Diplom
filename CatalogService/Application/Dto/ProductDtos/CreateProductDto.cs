using ProductService.Domain.Entities;

namespace ProductService.Application.Dto.ProductDtos;

public record CreateProductDto(string Name, string Description, decimal Price, List<Uri> Images, Category Category);
