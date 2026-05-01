using CondominiumManager.Identity.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CondominiumManager.Identity.Application.Auth;

internal class IdentityService(JwtConfiguration jwtConfig)
{
    private readonly JwtConfiguration _config = jwtConfig;

    public async Task<string> GenerateToken(User user)
    {
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), 
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
            new Claim(JwtRegisteredClaimNames.PreferredUsername, user.Name.ToString()),
            new Claim("permission", Permissions.HealthCheck),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config.Issuer,
            audience: _config.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_config.ExpireMinutes),
            signingCredentials: creds
            
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
