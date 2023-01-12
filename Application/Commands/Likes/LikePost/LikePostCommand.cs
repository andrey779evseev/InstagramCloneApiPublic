using MediatR;

namespace Application.Commands.Likes.LikePost;

public record LikePostCommand(Guid PostId) : IRequest;