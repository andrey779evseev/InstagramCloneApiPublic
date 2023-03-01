using Application.Commands.User.SetAvatar;
using Application.Commands.User.UpdateUser;
using Application.Queries.User.CheckNickname;
using Application.Queries.User.GetCurrentUser;
using Application.Queries.User.GetStats;
using Application.Queries.User.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.User;

[Authorize]
[Route("api/user")]
public class UserController : BaseController
{
    /// <summary>
    /// Check if user with this nickname already exist
    /// </summary>
    [AllowAnonymous]
    [HttpGet("check-nickname/{nickname}")]
    public async Task<IActionResult> CheckNickname(string nickname, CancellationToken cancellationToken)
    {
        var query = new CheckNicknameQuery(nickname);
        var valid = await Mediator.Send(query, cancellationToken);
        return Ok(valid);
    }

    /// <summary>
    /// Get authenticated user by provided access token
    /// </summary>
    [HttpGet("get-current-user")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var query = new GetCurrentUserQuery();
        var user = await Mediator.Send(query, cancellationToken);
        return Ok(user);
    }

    /// <summary>
    /// Get user by id
    /// </summary>
    [HttpGet("get-user/{userId:guid}")]
    public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(userId);
        var user = await Mediator.Send(query, cancellationToken);
        return Ok(user);
    }

    /// <summary>
    /// Set avatar for user
    /// </summary>
    [HttpPut("set-avatar")]
    public async Task<IActionResult> SetAvatar(SetAvatarCommand command, CancellationToken cancellationToken)
    {
        var url = await Mediator.Send(command, cancellationToken);
        return Ok(url);
    }

    /// <summary>
    /// Update user
    /// </summary>
    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await Mediator.Send(command, cancellationToken);
        return Ok(user);
    }

    /// <summary>
    /// Get account stats (posts count, followers count, following count)
    /// </summary>
    [HttpGet("get-stats/{userId:guid}")]
    public async Task<IActionResult> GetStats(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetStatsQuery(userId);
        var stats = await Mediator.Send(query, cancellationToken);
        return Ok(stats);
    }
}