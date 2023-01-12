using System.Security.Claims;
using Domain.Interfaces.Utils.Tokens;
using Domain.Models.User;
using Domain.Settings.Utils.Tokens;
using Domain.Utils.Tokens;

namespace Infrastructure.Utils.Tokens;

public class AccessTokenService : IAccessTokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly ITokenGenerator _tokenGenerator;

    public AccessTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings)
    {
        _tokenGenerator = tokenGenerator;
        _jwtSettings = jwtSettings;
    }

    public GenerateTokenResponse Generate(User user)
    {
        List<Claim> claims = new()
        {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
        };
        return _tokenGenerator.Generate(_jwtSettings.AccessTokenSecret, _jwtSettings.Issuer, _jwtSettings.Audience,
            _jwtSettings.AccessTokenExpirationMinutes, claims);
    }
}