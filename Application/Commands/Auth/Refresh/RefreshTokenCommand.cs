using Domain.Utils.Tokens;
using MediatR;

namespace Application.Commands.Auth.Refresh;

public record RefreshTokenCommand(string RefreshToken, string Email) : IRequest<AuthenticateResponse>;