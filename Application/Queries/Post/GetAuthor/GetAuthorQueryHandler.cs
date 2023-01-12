using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.User;
using MediatR;

namespace Application.Queries.Post.GetAuthor;

public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, UserMiniature>
{
    private readonly ILogger _logger;
    private readonly IPostRepository _postRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;

    public GetAuthorQueryHandler(
        IUserAccessor userAccessor,
        IPostRepository postRepository,
        IUserRepository userRepository,
        ILogger logger
    )
    {
        _userAccessor = userAccessor;
        _postRepository = postRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<UserMiniature> Handle(GetAuthorQuery query, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var post = await _postRepository.OneById(query.PostId, cancellationToken);
        if (post == null)
            throw new NotFoundException("post", "post id");

        var author = await _userRepository.OneMiniatureById(post.AuthorId, cancellationToken);
        if (author == null)
            throw new UserDoesNotExistException("author id");

        return author;
    }
}