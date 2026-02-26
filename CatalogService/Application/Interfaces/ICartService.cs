using ProductService.Application.Dto.CartDtos;
using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface ICartService
{
    public Task<Cart?> AddProductToCart(Product product, Guid cartId);
    public Task<Cart?> RemoveProductFromCart(Product product, Guid cartId);
    public Task<List<Product>> GetAllProductsFromCart(Guid cartId);
    public Task<List<Cart>> GetAllCarts();
    public Task<Cart> AddCart(CreateCartDto cartDto);
    public Task<bool> RemoveCart(Guid cartId);
    public Task<Cart?> UpdateCart(UpdateCartDto cartDto, Guid cartId);
}

