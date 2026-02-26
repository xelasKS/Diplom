using ProductService.Application.Dto.AuthDtos;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Interfaces;

public interface IAuthService
{
    public Task<string> Login(LoginDto user);
    public Task<bool> Register(RegisterDto user);
}