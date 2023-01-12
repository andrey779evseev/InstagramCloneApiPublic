using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Interfaces.Utils.Tokens;
using Domain.Utils.Tokens;
using MediatR;

namespace Application.Commands.Auth.Refresh;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticateResponse>
{
    private readonly IAuthenticateService _authenticateService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IUserRepository _userRepository;

    public RefreshTokenCommandHandler(
        IRefreshTokenService refreshTokenService,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IAuthenticateService authenticateService
    )
    {
        _refreshTokenService = refreshTokenService;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _authenticateService = authenticateService;
    }

    public async Task<AuthenticateResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var validByService = _refreshTokenService.Validate(command.RefreshToken);
        if (!validByService)
            throw new InvalidRefreshTokenException();

        var user = await _userRepository.OneByEmail(command.Email, cancellationToken);

        if (user == null)
            throw new InvalidRefreshTokenException("User for refresh token by provided email not found");

        var token = await _refreshTokenRepository.OneByToken(command.RefreshToken, cancellationToken);
        if (token == null || token.Revoked || DateTime.Compare(token.ExpiresAt, DateTime.Now) > 0)
            throw new InvalidRefreshTokenException();

        return await _authenticateService.Authenticate(user, cancellationToken, token);
    }
}