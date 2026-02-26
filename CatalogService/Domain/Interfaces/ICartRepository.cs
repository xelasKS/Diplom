using ProductService.Application.Dto.CartDtos;
using ProductService.Domain.Entities;

namespace ProductService.Domain.Interfaces;

public interface ICartRepository
{
    public Task<List<Cart>> GetAllAsync();
    public Task AddCartAsync(Cart cart);
    public Task<Cart?> GetCartAsync(Guid cartId,  Guid userId);
    public Task RemoveCartAsync(Cart cart);
    public Task UpdateCartAsync(Cart cart);
    public Task SaveChangesAsync();
}