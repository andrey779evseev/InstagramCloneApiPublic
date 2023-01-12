using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.Like;
using MediatR;

namespace Application.Queries.Likes.GetLikes;

public class GetLikesQueryHandler : IRequestHandler<GetLikesQuery, List<LikeDetail>>
{
    private readonly ILikeRepository _likeRepository;
    private readonly ILogger _logger;
    private readonly IPostRepository _postRepository;

    public GetLikesQueryHandler(
        IPostRepository postRepository,
        ILikeRepository likeRepository,
        ILogger logger
    )
    {
        _postRepository = postRepository;
        _likeRepository = likeRepository;
        _logger = logger;
    }

    public async Task<List<LikeDetail>> Handle(GetLikesQuery query, CancellationToken cancellationToken)
    {
        var exist = await _postRepository.ExistsWithId(query.PostId, cancellationToken);
        if (!exist)
            throw new NotFoundException("post", "id");

        var likes = await _likeRepository.FindDetailsByPost(query.PostId, cancellationToken);

        return likes;
    }
}