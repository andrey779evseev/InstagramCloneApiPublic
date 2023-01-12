using Application.Commands.Friendships.FollowUser;
using Application.Commands.Friendships.SearchUsers;
using Application.Commands.Friendships.UnfollowUser;
using Application.Queries.Friendships.GetFollowers;
using Application.Queries.Friendships.GetFollowing;
using Application.Queries.Friendships.GetSuggestions;
using Application.Queries.Friendships.IsFollowed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Friendships;

[Authorize]
[Route("api/friendships")]
public class FriendshipsController : BaseController
{
    /// <summary>
    /// Follow user
    /// </summary>
    [HttpPost("follow/{userId:guid}")]
    public async Task<IActionResult> FollowUser(Guid userId, CancellationToken cancellationToken)
    {
        var command = new FollowUserCommand(userId);
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Unfollow user
    /// </summary>
    [HttpPost("unfollow/{userId:guid}")]
    public async Task<IActionResult> UnfollowUser(Guid userId, CancellationToken cancellationToken)
    {
        var command = new UnfollowUserCommand(userId);
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Get users suggestions (for following)
    /// </summary>
    [HttpGet("get-suggestions")]
    public async Task<IActionResult> GetSuggestions([FromQuery] int? take, CancellationToken cancellationToken)
    {
        var query = new GetSuggestionsQuery(take ?? 15);
        var suggestions = await Mediator.Send(query, cancellationToken);
        return Ok(suggestions);
    }

    /// <summary>
    /// Get list of following users
    /// </summary>
    [HttpGet("get-following/{userId:guid}")]
    public async Task<IActionResult> GetFollowing(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetFollowingQuery(userId);
        var following = await Mediator.Send(query, cancellationToken);
        return Ok(following);
    }

    /// <summary>
    /// Get list of users who follow you
    /// </summary>
    [HttpGet("get-followers/{userId:guid}")]
    public async Task<IActionResult> GetFollowers(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetFollowersQuery(userId);
        var following = await Mediator.Send(query, cancellationToken);
        return Ok(following);
    }

    /// <summary>
    /// Is followed user with provided id by current user 
    /// </summary>
    [HttpGet("is-followed/{userId:guid}")]
    public async Task<IActionResult> IsFollowed(Guid userId, CancellationToken cancellationToken)
    {
        var query = new IsFollowedQuery(userId);
        var followed = await Mediator.Send(query, cancellationToken);
        return Ok(followed);
    }

    /// <summary>
    /// Search users 
    /// </summary>
    [HttpPost("search-users")]
    public async Task<IActionResult> SearchUsers(SearchUsersCommand command, CancellationToken cancellationToken)
    {
        var users = await Mediator.Send(command, cancellationToken);
        return Ok(users);
    }
}