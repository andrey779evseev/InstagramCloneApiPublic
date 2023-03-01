using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.GoogleTokenValidator;
using Domain.Interfaces.Utils.Logger;
using Domain.Interfaces.Utils.Tokens;
using Domain.Utils.Tokens;
using Infrastructure.Extensions;
using MediatR;

namespace Application.Commands.Auth.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, AuthenticateResponse>
{
    private readonly IAuthenticateService _authenticateService;
    private readonly IGoogleTokenValidator _googleTokenValidator;
    private readonly IUserRepository _userRepository;

    public GoogleLoginCommandHandler(
        IUserRepository userRepository,
        IAuthenticateService authenticateService,
        IGoogleTokenValidator googleTokenValidator
    )
    {
        _userRepository = userRepository;
        _authenticateService = authenticateService;
        _googleTokenValidator = googleTokenValidator;
    }

    public async Task<AuthenticateResponse> Handle(GoogleLoginCommand command, CancellationToken cancellationToken)
    {
        var googleId = await _googleTokenValidator.GetId(command.Token);
        if (googleId == null)
            throw new InvalidUserCredentialsException();

        var user = await _userRepository.OneByGoogleId(googleId, cancellationToken);
        if (user != null)
            return await _authenticateService.Authenticate(user, cancellationToken);

        return await RegisterUser(command, googleId, cancellationToken);
    }

    private async Task<AuthenticateResponse> RegisterUser(GoogleLoginCommand command, string googleId,
        CancellationToken cancellationToken)
    {
        var existWithEmail = await _userRepository.ExistsWithEmail(command.Email, cancellationToken);
        if (existWithEmail)
            throw new InvalidLoginMethodException(
                "This email is already taken by another account. Please provide this email and password in form above");

        var nickname = await GetNickname(command.Name, cancellationToken);

        var user = new Domain.Models.User.User(
            command.Name,
            command.Email,
            nickname,
            null,
            googleId
        );

        if (command.Avatar != null)
            user.SetAvatar(command.Avatar);

        await _userRepository.Save(user, cancellationToken);

        return await _authenticateService.Authenticate(user, cancellationToken);
    }

    private async Task<string> GetNickname(string name, CancellationToken cancellationToken)
    {
        var nickname = "";
        var isCheckedName = false;
        do
        {
            var tempNickname = name
                .ToLower()
                .Trim()
                .ConvertToLatin()
                .Replace(" ", "_");
            if (!isCheckedName)
            {
                var exist = await _userRepository.ExistsWithNickname(tempNickname, cancellationToken);
                if (!exist)
                    nickname = tempNickname;
                else
                    isCheckedName = true;
            }
            else
            {
                tempNickname = $"{tempNickname}{new Random().Next(1000, 9999)}";
                var exist = await _userRepository.ExistsWithNickname(tempNickname, cancellationToken);
                if (!exist)
                    nickname = tempNickname;
            }
        } while (nickname == "");

        return nickname;
    }
}