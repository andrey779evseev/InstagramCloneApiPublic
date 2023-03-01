using MediatR;

namespace Application.Queries.Friendships.IsFollowed;

public record IsFollowedQuery(Guid UserId) : IRequest<bool>;