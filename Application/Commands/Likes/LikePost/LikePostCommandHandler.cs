using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.Like;
using MediatR;

namespace Application.Commands.Likes.LikePost;

public class LikePostCommandHandler : IRequestHandler<LikePostCommand>
{
    private readonly ILikeRepository _likeRepository;
    private readonly ILogger _logger;
    private readonly IPostRepository _postRepository;
    private readonly IUserAccessor _userAccessor;

    public LikePostCommandHandler(
        IUserAccessor userAccessor,
        IPostRepository postRepository,
        ILogger logger,
        ILikeRepository likeRepository
    )
    {
        _userAccessor = userAccessor;
        _postRepository = postRepository;
        _logger = logger;
        _likeRepository = likeRepository;
    }

    public async Task<Unit> Handle(LikePostCommand command, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var post = await _postRepository.OneById(command.PostId, cancellationToken);
        if (post == null)
            throw new NotFoundException("Post", "id");

        var liked = await _likeRepository.ExistsWithPostAndUser(user.Id, post.Id, cancellationToken);
        if (liked)
            throw new EntityExistsException("Like", "post id and user id");

        var like = new Like(user.Id, post.Id);

        await _likeRepository.Save(like, cancellationToken);

        return Unit.Value;
    }
}