using Domain.Models.User;
using MediatR;

namespace Application.Queries.Friendships.GetFollowers;

public record GetFollowersQuery(Guid UserId) : IRequest<List<UserMiniature>>;