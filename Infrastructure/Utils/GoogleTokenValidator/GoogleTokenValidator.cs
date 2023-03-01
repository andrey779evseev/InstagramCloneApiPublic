using Domain.Interfaces.Utils.GoogleTokenValidator;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Utils.GoogleTokenValidator;

public class GoogleTokenValidator : IGoogleTokenValidator
{
    private readonly IConfiguration _config;

    public GoogleTokenValidator(IConfiguration config)
    {
        _config = config;
    }
    public async Task<string?> GetId(string token)
    {
        var valid = true;
        try
        {
            var clientId = _config.GetSection("GOOGLE_SIGNIN_CLIENT_ID").Value;
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);
            if (!payload.Audience.Equals(clientId))
                valid = false;
            if (!payload.Issuer.Equals("accounts.google.com") && !payload.Issuer.Equals("https://accounts.google.com"))
                valid = false;
            if (payload.ExpirationTimeSeconds == null)            
                valid = false;            
            else
            {
                var now = DateTime.Now.ToUniversalTime();
                var expiration = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).DateTime;
                if (now > expiration)
                {
                    valid = false;
                }
            }

            return valid ? payload.Subject : null;
        }
        catch (InvalidJwtException)
        {
            return null;
        }
    }
}