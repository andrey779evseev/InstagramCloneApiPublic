using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using MediatR;

namespace Application.Commands.Auth.Revoke;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    public RevokeTokenCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository
    )
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RevokeTokenCommand tokenCommand, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenRepository.OneByToken(tokenCommand.RefreshToken, cancellationToken);

        if (token == null)
            throw new NotFoundException("Refresh Token", "token");
        var user = await _userRepository.OneById(token.UserId, cancellationToken);
        if (user == null)
            throw new UserDoesNotExistException("id");

        if (token.Revoked)
            throw new TokenAlreadyRevokedException();

        token.Revoke();
        await _refreshTokenRepository.Save(token, cancellationToken);
        return Unit.Value;
    }
}