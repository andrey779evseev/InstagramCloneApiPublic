using MediatR;

namespace Application.Commands.Comments.AddPostComment;

public record AddPostCommentCommand(Guid PostId, string Text) : IRequest;