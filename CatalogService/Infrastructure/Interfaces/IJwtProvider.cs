using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Interfaces;

public interface IJwtProvider
{
    public string GenerateToken(User user);
    public string RefreshToken(string token);
}