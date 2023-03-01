using Domain.Models.Comment;
using MediatR;

namespace Application.Queries.Comments.GetFirstComment;

public record GetFirstCommentQuery(Guid PostId) : IRequest<CommentMiniature?>;