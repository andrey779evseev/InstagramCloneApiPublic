using Domain.Models.Post;
using MediatR;

namespace Application.Queries.Posts.GetMiniatures;

public record GetMiniaturesQuery(Guid UserId, DateTime? Cursor, int Take) : IRequest<List<PostMiniature>>;