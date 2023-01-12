using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using MediatR;

namespace Application.Commands.Post.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
{
    private readonly ILogger _logger;
    private readonly IPostRepository _postRepository;
    private readonly IUserAccessor _userAccessor;

    public CreatePostCommandHandler(
        IUserAccessor userAccessor,
        IPostRepository postRepository,
        ILogger logger
    )
    {
        _userAccessor = userAccessor;
        _postRepository = postRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var post = new Domain.Models.Post.Post(user.Id, command.Description, command.Photo);

        await _postRepository.Save(post, cancellationToken);

        return Unit.Value;
    }
}