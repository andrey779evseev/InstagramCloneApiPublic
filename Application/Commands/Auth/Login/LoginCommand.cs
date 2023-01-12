using Domain.Utils.Tokens;
using MediatR;

namespace Application.Commands.Auth.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthenticateResponse>;