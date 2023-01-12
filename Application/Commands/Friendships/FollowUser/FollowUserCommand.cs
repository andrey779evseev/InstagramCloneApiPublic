using MediatR;

namespace Application.Commands.Friendships.FollowUser;

public record FollowUserCommand(Guid UserId) : IRequest;