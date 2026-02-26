using ProductService.Domain.Entities;

namespace ProductService.Application.Dto.CartDtos;

public record UpdateCartDto(List<Product> Products);