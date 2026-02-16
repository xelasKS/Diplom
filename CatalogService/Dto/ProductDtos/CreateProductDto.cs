using ProductService.Entities;

namespace ProductService.Dto.ProductDtos;

public record CreateProductDto(string Name, string Description, decimal Price, List<Uri> Images, Category Category);
