using Microsoft.EntityFrameworkCore;
using ProductService.Application.Dto.AuthDtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Interfaces;

namespace ProductService.Application.Services;

public class AuthService(IPasswordHasher passwordHasher, ApplicationContext context, IJwtProvider jwtProvider) : IAuthService
{
    public async Task<string> Login(LoginDto dto)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (user is null)
        {
            throw new Exception("User not found");
        }
        if(!passwordHasher.Verify(dto.Password, user.HashPassword))
        {
            throw new Exception("Password doesn't match");
        }

        return jwtProvider.GenerateToken(user);
    }

    public async Task<bool> Register(RegisterDto dto)
    {
        if (await context.Users.AsNoTracking().AnyAsync(u => u.Username == dto.Username))
        {
            return false;
        }
        var passwordHash = passwordHasher.Generate(dto.Password);
        var user = new User(dto.Username, passwordHash, dto.Email);
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return true;
    }
}