using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.Comment;
using MediatR;

namespace Application.Queries.Comments.GetComments;

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, List<CommentDetail>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ILogger _logger;

    public GetCommentsQueryHandler(
        ICommentRepository commentRepository,
        ILogger logger
    )
    {
        _commentRepository = commentRepository;
        _logger = logger;
    }

    public async Task<List<CommentDetail>> Handle(GetCommentsQuery query, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.FindByPost(query.PostId, cancellationToken);

        return comments;
    }
}