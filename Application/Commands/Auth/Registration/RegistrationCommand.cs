using MediatR;

namespace Application.Commands.Auth.Registration;

public record RegistrationCommand(
    string Name,
    string Email,
    string Password,
    string Nickname
) : IRequest;