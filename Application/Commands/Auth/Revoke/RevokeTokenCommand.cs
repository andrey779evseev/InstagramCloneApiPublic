using MediatR;

namespace Application.Commands.Auth.Revoke;

public record RevokeTokenCommand(string RefreshToken) : IRequest;