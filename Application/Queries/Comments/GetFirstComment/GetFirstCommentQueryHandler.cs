using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.Comment;
using MediatR;

namespace Application.Queries.Comments.GetFirstComment;

public class GetFirstCommentQueryHandler : IRequestHandler<GetFirstCommentQuery, CommentMiniature?>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ILogger _logger;
    private readonly IPostRepository _postRepository;

    public GetFirstCommentQueryHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        ILogger logger
    )
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _logger = logger;
    }

    public async Task<CommentMiniature?> Handle(GetFirstCommentQuery query, CancellationToken cancellationToken)
    {
        var post = await _postRepository.OneById(query.PostId, cancellationToken);
        if (post == null)
            throw new NotFoundException("Post", "id");

        var comment = await _commentRepository.OneByPost(query.PostId, cancellationToken);

        return comment;
    }
}