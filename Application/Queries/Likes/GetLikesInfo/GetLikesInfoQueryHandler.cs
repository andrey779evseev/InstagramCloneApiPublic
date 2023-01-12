using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.Like;
using MediatR;

namespace Application.Queries.Likes.GetLikesInfo;

public class GetLikesInfoQueryHandler : IRequestHandler<GetLikesInfoQuery, PostLikesInfo>
{
    private readonly ILikeRepository _likeRepository;
    private readonly ILogger _logger;
    private readonly IUserAccessor _userAccessor;

    public GetLikesInfoQueryHandler(
        ILikeRepository likeRepository,
        IUserAccessor userAccessor,
        ILogger logger
    )
    {
        _likeRepository = likeRepository;
        _userAccessor = userAccessor;
        _logger = logger;
    }

    public async Task<PostLikesInfo> Handle(GetLikesInfoQuery query, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var firstNickname = await _likeRepository.FirstNicknameByPost(query.PostId, cancellationToken);
        var firstAvatars = await _likeRepository.FirstAvatarsByPost(query.PostId, cancellationToken);

        var likesCount = await _likeRepository.CountByPost(query.PostId, cancellationToken);

        var liked = await _likeRepository.ExistsWithPostAndUser(user.Id, query.PostId, cancellationToken);

        var likesInfo = new PostLikesInfo(
            firstNickname,
            firstAvatars,
            likesCount,
            liked
        );

        return likesInfo;
    }
}