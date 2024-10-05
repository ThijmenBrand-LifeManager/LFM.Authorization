using System.Security.Claims;
using System.Text;
using LFM.Authorization.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace LFM.Authorization.Core.AuthHelpers;

public sealed class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string CreateToken(LfmUser user)
    {
        var secret = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt").GetValue<string>("Secret") ??
                                            throw new NullReferenceException());
        var securityKey = new SymmetricSecurityKey(secret);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("email_verified", user.EmailConfirmed.ToString()),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = _configuration.GetValue<string>("Jwt:Issuer"),
            Audience = _configuration.GetValue<string>("Jwt:Audience"),
        };

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}