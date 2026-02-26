using ProductService.Domain.Entities;
using ProductService.Infrastructure.Data;

namespace ProductService.Domain.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAll();
    public Task<User?> GetUserByIdAsync(Guid userId);
    public Task<User?> GetUserByUsernameAsync(string username);
    public Task<User?> GetUserByEmailAsync(string email);
    
}