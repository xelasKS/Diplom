using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductService.Application.Interfaces;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Interfaces;

namespace ProductService.Infrastructure.Identity;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions  _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    public string GenerateToken(User user)
    {
        var claims = new Claim[]
        {
            new Claim("userID", user.Id.ToString()),
            new Claim("userEmail", user.Email),
            new Claim("username", user.Username),
        };
        var jwt = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours),
            claims: claims,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public string RefreshToken(string token)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var newJwt = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours),
            claims: jwt.Claims,
            signingCredentials: jwt.SigningCredentials);

        return new JwtSecurityTokenHandler().WriteToken(newJwt);
    }
}