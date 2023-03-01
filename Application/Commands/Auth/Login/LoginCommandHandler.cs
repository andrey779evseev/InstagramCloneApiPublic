using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Interfaces.Utils.PasswordHasher;
using Domain.Interfaces.Utils.Tokens;
using Domain.Utils.Tokens;
using MediatR;

namespace Application.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticateResponse>
{
    private readonly IAuthenticateService _authenticateService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(
        IAuthenticateService authenticateService,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher
    )
    {
        _authenticateService = authenticateService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticateResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OneByEmail(command.Email, cancellationToken);
        if (user == null)
            throw new InvalidUserCredentialsException();

        if (user.GoogleId != null)
            throw new InvalidLoginMethodException(
                "This account created via google login. Click 'Continue with google'");

        var valid = _passwordHasher.Check(user.HashedPassword!, command.Password);

        if (!valid)
            throw new InvalidUserCredentialsException();

        return await _authenticateService.Authenticate(user, cancellationToken);
    }
}