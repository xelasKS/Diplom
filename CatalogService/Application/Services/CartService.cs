
using ProductService.Application.Dto.CartDtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Services;

public class CartService(ICartRepository cartRepository, Guid userId) : ICartService
{
    public async Task<Cart?> AddProductToCart(Product product, Guid cartId)
    {
        var cart = await cartRepository.GetCartAsync(cartId,  userId);
        if (cart is not null)
        {
            cart.Products.Add(product);
            await cartRepository.SaveChangesAsync();
        }
        return cart;
    }

    public async Task<Cart?> RemoveProductFromCart(Product product, Guid cartId)
    {
        var cart = await cartRepository.GetCartAsync(cartId, userId);
        if (cart is not null)
        {
            cart.Products.Remove(product);
            await cartRepository.SaveChangesAsync();
        }
        return cart;
    }

    public async Task<List<Product>> GetAllProductsFromCart(Guid cartId)
    {
        var cart = await cartRepository.GetCartAsync(cartId, userId);
        if (cart is not null)
        {
            return cart.Products;
        }
        return new List<Product>();
    }

    public async Task<List<Cart>> GetAllCarts()
    {
        return await cartRepository.GetAllAsync();
    }

    public async Task<Cart> AddCart(CreateCartDto cartDto)
    {
        var cart = new Cart(cartDto.Products, userId);
        await cartRepository.AddCartAsync(cart);
        return cart;
    }

    public async Task<bool> RemoveCart(Guid cartId)
    {
        var cart = await cartRepository.GetCartAsync(cartId, userId);
        if (cart is not null)
        {
            await cartRepository.RemoveCartAsync(cart);
            return true;
        }
        return false;
    }

    public async Task<Cart?> UpdateCart(UpdateCartDto cartDto, Guid cartId)
    {
        var cart = await cartRepository.GetCartAsync(cartId, userId);
        if (cart is not null)
        {
            cart.Products = cartDto.Products;
            await cartRepository.UpdateCartAsync(cart);
        }
        return cart;
        
    }
    
}