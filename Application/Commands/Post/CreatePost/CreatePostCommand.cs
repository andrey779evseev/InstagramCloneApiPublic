using MediatR;

namespace Application.Commands.Post.CreatePost;

public record CreatePostCommand(string Photo, string Description) : IRequest;