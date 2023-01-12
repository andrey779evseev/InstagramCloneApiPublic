using MediatR;

namespace Application.Queries.Posts.GetFeed;

public record GetFeedQuery(DateTime? Cursor, int Take) : IRequest<List<Domain.Models.Post.Post>>;