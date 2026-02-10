using CatalogService.Models;

namespace ProductService.Dto.DiscountDtos;

public record CreateDiscountDto(List<Product> Products,  byte Percent);
