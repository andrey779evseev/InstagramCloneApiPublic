using Domain.Models.User;
using MediatR;

namespace Application.Queries.Friendships.GetFollowing;

public record GetFollowingQuery(Guid UserId) : IRequest<List<UserMiniature>>;