using ProductService.Domain.Entities;

namespace ProductService.Application.Dto.CartDtos;

public record CreateCartDto(List<Product> Products);