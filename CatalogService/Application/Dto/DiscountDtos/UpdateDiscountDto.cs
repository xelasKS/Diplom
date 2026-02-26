using ProductService.Domain.Entities;

namespace ProductService.Application.Dto.DiscountDtos;

public record UpdateDiscountDto(List<Product>? Products, byte? Percentage);