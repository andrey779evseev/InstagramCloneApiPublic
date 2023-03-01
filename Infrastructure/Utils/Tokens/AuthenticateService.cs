using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Tokens;
using Domain.Models.RefreshToken;
using Domain.Models.User;
using Domain.Utils.Tokens;

namespace Infrastructure.Utils.Tokens;

public class AuthenticateService : IAuthenticateService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthenticateService(
        IRefreshTokenService refreshTokenService,
        IAccessTokenService accessTokenService,
        IRefreshTokenRepository refreshTokenRepository
    )
    {
        _refreshTokenService = refreshTokenService;
        _accessTokenService = accessTokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<AuthenticateResponse> Authenticate(
        User user,
        CancellationToken cancellationToken,
        RefreshToken? validRefreshToken = null
    )
    {
        if (validRefreshToken != null)
        {
            validRefreshToken.Revoke();
            await _refreshTokenRepository.Save(validRefreshToken, cancellationToken);
        }

        var refreshToken = _refreshTokenService.Generate(user);
        var accessToken = _accessTokenService.Generate(user);
        var token = new RefreshToken(refreshToken.Token, user.Id, refreshToken.ExpiresAt);
        await _refreshTokenRepository.Save(token, cancellationToken);
        return new AuthenticateResponse
        (
            accessToken.Token,
            refreshToken.Token,
            accessToken.ExpiresAt,
            refreshToken.ExpiresAt
        );
    }
}