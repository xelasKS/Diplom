using CatalogService.Models;

namespace ProductService.Dto.DiscountDtos;

public record UpdateDiscountDto(List<Product>? Products, byte? Percentage);