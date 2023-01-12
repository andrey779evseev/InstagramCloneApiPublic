using Application.Commands.Auth.ChangePassword;
using Application.Commands.Auth.GoogleLogin;
using Application.Commands.Auth.Login;
using Application.Commands.Auth.Refresh;
using Application.Commands.Auth.Registration;
using Application.Commands.Auth.Revoke;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers.Auth;

[AllowAnonymous]
[Route("api/auth")]
public class AuthController : BaseController
{
    /// <summary>
    /// Register user by email
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult> Registration(RegistrationCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Login with user credentials
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken cancellationToken)
    {
        var credentials = await Mediator.Send(command, cancellationToken);
        return Ok(credentials);
    }

    /// <summary>
    /// Registration/Login with google account
    /// </summary>
    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginCommand command, CancellationToken cancellationToken)
    {
        var credentials = await Mediator.Send(command, cancellationToken);
        return Ok(credentials);
    }

    /// <summary>
    /// Refresh access token with refresh token, provided via authentication
    /// </summary>
    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh(RefreshTokenCommand tokenCommand, CancellationToken cancellationToken)
    {
        var credentials = await Mediator.Send(tokenCommand, cancellationToken);
        return Ok(credentials);
    }

    /// <summary>
    /// Revoke refresh token (when logout)
    /// </summary>
    [HttpPost("revoke-token")]
    public async Task<IActionResult> Revoke(RevokeTokenCommand tokenCommand, CancellationToken cancellationToken)
    {
        await Mediator.Send(tokenCommand, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Change user password (except google users)
    /// </summary>
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }
}