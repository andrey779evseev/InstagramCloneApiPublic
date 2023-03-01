namespace Domain.Interfaces.Utils.Tokens;

public interface IRefreshTokenService : ITokenService
{
    bool Validate(string refreshToken);
}