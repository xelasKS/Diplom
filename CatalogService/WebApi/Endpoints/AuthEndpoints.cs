using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Dto.AuthDtos;
using ProductService.Application.Interfaces;

namespace ProductService.WebApi.Endpoints;

public class AuthEndpoints
{
    private static async Task<IResult> Login([FromBody]LoginDto dto, IAuthService authService, HttpContext context)
    {
        var token =  await authService.Login(dto);
        context.Response.Cookies.Append("token-auth", token);
        return Results.Ok();
    }

    private static async Task<IResult> Register([FromBody]RegisterDto dto, IAuthService authService)
    {
        var res = await authService.Register(dto);
        return res ?  Results.Ok() : Results.BadRequest();
    }

    public static void MapAuth(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/users");
        group.MapGet("/login", Login);
        group.MapPost("/register", Register);
    } 
}