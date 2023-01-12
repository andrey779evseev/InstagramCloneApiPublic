using Domain.Utils.Tokens;
using MediatR;

namespace Application.Commands.Auth.GoogleLogin;

public record GoogleLoginCommand(
    string Name,
    string Email,
    string? Avatar,
    string Token
) : IRequest<AuthenticateResponse>;