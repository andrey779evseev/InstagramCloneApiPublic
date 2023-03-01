using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Interfaces.Utils.Tokens;
using Domain.Utils.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Utils.Tokens;

public class TokenGenerator : ITokenGenerator
{
    public GenerateTokenResponse Generate(string secretKey, string issuer, string audience, double expires,
        IEnumerable<Claim>? claims = null)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.Now.AddMinutes(expires);
        JwtSecurityToken securityToken = new(issuer, audience,
            claims,
            DateTime.Now,
            expiresAt,
            credentials);
        return new GenerateTokenResponse(
            new JwtSecurityTokenHandler().WriteToken(securityToken),
            expiresAt
        );
    }
}