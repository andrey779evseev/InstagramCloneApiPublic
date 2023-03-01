using Domain.Models.Like;
using MediatR;

namespace Application.Queries.Likes.GetLikes;

public record GetLikesQuery(Guid PostId) : IRequest<List<LikeDetail>>;