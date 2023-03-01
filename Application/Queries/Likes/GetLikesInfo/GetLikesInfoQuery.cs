using Domain.Models.Like;
using MediatR;

namespace Application.Queries.Likes.GetLikesInfo;

public record GetLikesInfoQuery(Guid PostId) : IRequest<PostLikesInfo>;