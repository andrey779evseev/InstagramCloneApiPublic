using MediatR;

namespace Application.Queries.Comments.GetCommentsCount;

public record GetCommentsCountQuery(Guid PostId) : IRequest<int>;