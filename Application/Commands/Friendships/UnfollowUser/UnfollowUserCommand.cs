using MediatR;

namespace Application.Commands.Friendships.UnfollowUser;

public record UnfollowUserCommand(Guid UserId) : IRequest;