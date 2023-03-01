using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Queries.Posts.GetFeed;

public class GetFeedQueryHandler : IRequestHandler<GetFeedQuery, List<Domain.Models.Post.Post>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserAccessor _userAccessor;

    public GetFeedQueryHandler(
        IUserAccessor userAccessor,
        IPostRepository postRepository
    )
    {
        _userAccessor = userAccessor;
        _postRepository = postRepository;
    }

    public async Task<List<Domain.Models.Post.Post>> Handle(GetFeedQuery query, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var posts = await _postRepository.FindForFeed(
            user.Following,
            query.Cursor,
            query.Take,
            cancellationToken
        );

        return posts;
    }
}