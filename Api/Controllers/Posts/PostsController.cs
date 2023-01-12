using Application.Queries.Posts.GetFeed;
using Application.Queries.Posts.GetMiniatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Posts;

[Authorize]
[Route("api/posts")]
public class PostsController : BaseController
{
    /// <summary>
    /// Get posts miniatures
    /// </summary>
    [HttpGet("get-miniatures/{userId:guid}")]
    public async Task<IActionResult> GetMiniatures(
        Guid userId,
        [FromQuery] DateTime? cursor,
        [FromQuery] int? take,
        CancellationToken cancellationToken
    )
    {
        var query = new GetMiniaturesQuery(userId, cursor, take ?? 15);
        var posts = await Mediator.Send(query, cancellationToken);
        return Ok(posts);
    }

    /// <summary>
    /// Get posts for feed
    /// </summary>
    [HttpGet("get-feed")]
    public async Task<IActionResult> GetFeed(
        [FromQuery] DateTime? cursor,
        [FromQuery] int? take,
        CancellationToken cancellationToken
    )
    {
        var query = new GetFeedQuery(cursor, take ?? 5);
        var detailedPosts = await Mediator.Send(query, cancellationToken);
        return Ok(detailedPosts);
    }
}