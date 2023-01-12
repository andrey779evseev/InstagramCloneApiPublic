using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Interfaces.Utils.PasswordHasher;
using MediatR;

namespace Application.Commands.Auth.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;

    public ChangePasswordCommandHandler(
        IPasswordHasher passwordHasher,
        IUserAccessor userAccessor,
        IUserRepository userRepository
    )
    {
        _passwordHasher = passwordHasher;
        _userAccessor = userAccessor;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        if (user.HashedPassword == null && user.GoogleId != null)
            throw new InvalidLoginMethodException("You can't change password for account created via google login");

        var validOldPassword = _passwordHasher.Check(user.HashedPassword!, command.OldPassword);
        if (!validOldPassword)
            throw new Exception("Provided old password doesn't equal to current");

        var newPasswordHash = _passwordHasher.Hash(command.NewPassword);

        user.UpdatePassword(newPasswordHash);

        await _userRepository.Save(user, cancellationToken);

        return Unit.Value;
    }
}