using System.Security.Claims;
using Domain.Utils.Tokens;

namespace Domain.Interfaces.Utils.Tokens;

public interface ITokenGenerator
{
    GenerateTokenResponse Generate(string secretKey, string issuer, string audience, double expires, IEnumerable<Claim>? claims = null);
}