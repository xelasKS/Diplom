using ProductService.Domain.Entities;

namespace ProductService.Application.Dto.DiscountDtos;

public record CreateDiscountDto(List<Product> Products,  byte Percent);
