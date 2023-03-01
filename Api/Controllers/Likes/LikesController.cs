using Application.Commands.Likes.LikePost;
using Application.Commands.Likes.UnlikePost;
using Application.Queries.Likes.GetLikes;
using Application.Queries.Likes.GetLikesInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Likes;

[Authorize]
[Route("api/likes")]
public class LikesController : BaseController
{
    /// <summary>
    /// Like post
    /// </summary>
    [HttpPost("like/{postId:guid}")]
    public async Task<IActionResult> LikePost(Guid postId, CancellationToken cancellationToken)
    {
        var command = new LikePostCommand(postId);
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Unlike post
    /// </summary>
    [HttpPost("unlike/{postId:guid}")]
    public async Task<IActionResult> UnlikePost(Guid postId, CancellationToken cancellationToken)
    {
        var command = new UnlikePostCommand(postId);
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Get likes info for post
    /// </summary>
    [HttpGet("get-likes-info/{postId:guid}")]
    public async Task<IActionResult> GetLikesInfo(Guid postId, CancellationToken cancellationToken)
    {
        var query = new GetLikesInfoQuery(postId);
        var likesInfo = await Mediator.Send(query, cancellationToken);
        return Ok(likesInfo);
    }

    /// <summary>
    /// Get list of users who liked this post
    /// </summary>
    [HttpGet("get-likes/{postId:guid}")]
    public async Task<IActionResult> GetLikes(Guid postId, CancellationToken cancellationToken)
    {
        var query = new GetLikesQuery(postId);
        var likes = await Mediator.Send(query, cancellationToken);
        return Ok(likes);
    }
}