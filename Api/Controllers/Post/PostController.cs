using Application.Commands.Post.CreatePost;
using Application.Queries.Post.GetAuthor;
using Application.Queries.Post.GetPost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Post;

[Authorize]
[Route("api/post")]
public class PostController : BaseController
{
    /// <summary>
    /// Create post
    /// </summary>
    [HttpPost("create-post")]
    public async Task<IActionResult> CreatePost(CreatePostCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Get post by id
    /// </summary>
    [HttpGet("get-post/{postId:guid}")]
    public async Task<IActionResult> GetPost(Guid postId, CancellationToken cancellationToken)
    {
        var query = new GetPostQuery(postId);
        var post = await Mediator.Send(query, cancellationToken);
        return Ok(post);
    }

    /// <summary>
    /// Get Author for post
    /// </summary>
    [HttpGet("get-author/{postId:guid}")]
    public async Task<IActionResult> GetAuthor(Guid postId, CancellationToken cancellationToken)
    {
        var query = new GetAuthorQuery(postId);
        var author = await Mediator.Send(query, cancellationToken);
        return Ok(author);
    }
}