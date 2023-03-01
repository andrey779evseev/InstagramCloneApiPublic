using MediatR;

namespace Application.Commands.Likes.UnlikePost;

public record UnlikePostCommand(Guid PostId) : IRequest;