using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using MediatR;

namespace Application.Queries.Comments.GetCommentsCount;

public class GetCommentsCountQueryHandler : IRequestHandler<GetCommentsCountQuery, int>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ILogger _logger;
    private readonly IPostRepository _postRepository;

    public GetCommentsCountQueryHandler(
        ICommentRepository commentRepository,
        IPostRepository postRepository,
        ILogger logger
    )
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _logger = logger;
    }

    public async Task<int> Handle(GetCommentsCountQuery query, CancellationToken cancellationToken)
    {
        var post = await _postRepository.OneById(query.PostId, cancellationToken);
        if (post == null)
            throw new NotFoundException("Post", "post id");

        var count = await _commentRepository.CountByPost(post.Id, cancellationToken);

        return count;
    }
}