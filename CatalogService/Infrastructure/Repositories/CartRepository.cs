using Microsoft.EntityFrameworkCore;
using ProductService.Application.Dto.CartDtos;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private ApplicationContext _context;

    public CartRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Cart>> GetAllAsync()
    {
        return await _context.Carts.ToListAsync();    
    }

    public async Task AddCartAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await SaveChangesAsync();
    }

    public async Task RemoveCartAsync(Cart cart)
    {
        _context.Carts.Remove(cart);
        await SaveChangesAsync();
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await SaveChangesAsync();
    }

    public async Task<Cart?> GetCartAsync(Guid cartId, Guid userId)
    {
        return await _context.Carts.Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == cartId && c.OwnerId == userId);
    }

    public async Task SaveChangesAsync()
    { 
        await _context.SaveChangesAsync();
    }
}