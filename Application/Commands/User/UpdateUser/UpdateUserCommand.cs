using MediatR;

namespace Application.Commands.User.UpdateUser;

public record UpdateUserCommand(
    string Name,
    string Nickname,
    string? Description,
    string Email,
    string? Gender
) : IRequest<Domain.Models.User.User>;