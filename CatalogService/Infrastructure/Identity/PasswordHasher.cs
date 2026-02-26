using ProductService.Application.Interfaces;
using ProductService.Infrastructure.Interfaces;

namespace ProductService.Infrastructure.Identity;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password)
    {
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        return password;
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}