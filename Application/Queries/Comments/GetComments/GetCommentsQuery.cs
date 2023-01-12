using Domain.Models.Comment;
using MediatR;

namespace Application.Queries.Comments.GetComments;

public record GetCommentsQuery(Guid PostId) : IRequest<List<CommentDetail>>;