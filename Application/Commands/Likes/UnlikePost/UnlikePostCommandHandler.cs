using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using MediatR;

namespace Application.Commands.Likes.UnlikePost;

public class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommand>
{
    private readonly ILikeRepository _likeRepository;
    private readonly ILogger _logger;
    private readonly IPostRepository _postRepository;
    private readonly IUserAccessor _userAccessor;

    public UnlikePostCommandHandler(
        IUserAccessor userAccessor,
        IPostRepository postRepository,
        ILikeRepository likeRepository,
        ILogger logger
    )
    {
        _userAccessor = userAccessor;
        _postRepository = postRepository;
        _likeRepository = likeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UnlikePostCommand command, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var post = await _postRepository.OneById(command.PostId, cancellationToken);
        if (post == null)
            throw new NotFoundException("Post", "id");

        var like = await _likeRepository.OneByPostAndUser(user.Id, post.Id, cancellationToken);
        if (like == null)
            throw new NotFoundException("Like", "post id and user id");

        await _likeRepository.Delete(like, cancellationToken);

        return Unit.Value;
    }
}