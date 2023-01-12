using Application.Commands.Comments.AddPostComment;
using Application.Queries.Comments.GetComments;
using Application.Queries.Comments.GetCommentsCount;
using Application.Queries.Comments.GetFirstComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Comments;

[Authorize]
[Route("api/comments")]
public class PostController : BaseController
{
    /// <summary>
    /// Add comment for post
    /// </summary>
    [HttpPost("add-comment/{postId:guid}")]
    public async Task<IActionResult> AddPostComment(Guid postId, [FromBody] string text,
        CancellationToken cancellationToken)
    {
        var command = new AddPostCommentCommand(postId, text);
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Get all comments for post
    /// </summary>
    [HttpGet("get-comments/{postId:guid}")]
    public async Task<IActionResult> GetComments(Guid postId, CancellationToken cancellationToken)
    {
        var query = new GetCommentsQuery(postId);
        var comments = await Mediator.Send(query, cancellationToken);
        return Ok(comments);
    }

    /// <summary>
    /// Get first comment for post
    /// </summary>
    [HttpGet("get-first-comment/{postId:guid}")]
    public async Task<IActionResult> GetFirstComment(Guid postId, CancellationToken cancellationToken)
    {
        var query = new GetFirstCommentQuery(postId);
        var comment = await Mediator.Send(query, cancellationToken);
        return Ok(comment);
    }

    /// <summary>
    /// Get first comments count for post
    /// </summary>
    [HttpGet("get-comments-count/{postId:guid}")]
    public async Task<IActionResult> GetCommentsCount(Guid postId, CancellationToken cancellationToken)
    {
        var query = new GetCommentsCountQuery(postId);
        var count = await Mediator.Send(query, cancellationToken);
        return Ok(count);
    }
}