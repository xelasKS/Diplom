using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductService.Application.Interfaces;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Interfaces;
using ProductService.WebApi.Endpoints;

namespace ProductService.WebApi.Extensions;

public static class ApiExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddDbContext<ApplicationContext>();
        services.AddScoped<IPasswordHasher>();
        services.AddScoped<IJwtProvider>();
        services.ConfigureOptions<JwtOptions>();
    }

    public static void ConfigureEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDiscounts();
        endpoints.MapCategories();
        endpoints.MapProducts();
    }

    public static void AddAppAuthentification(this IServiceCollection services, IConfiguration configuration, IOptions<JwtOptions> jwtOptions)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token-auth"];
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization();
    }
}