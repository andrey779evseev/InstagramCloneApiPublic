using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Interfaces.Utils.PasswordHasher;
using MediatR;

namespace Application.Commands.Auth.Registration;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand>
{
    private readonly IPasswordHasher _hasher;
    private readonly IUserRepository _userRepository;

    public RegistrationCommandHandler(
        IPasswordHasher hasher,
        IUserRepository userRepository
    )
    {
        _hasher = hasher;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RegistrationCommand command, CancellationToken cancellationToken)
    {
        var withEmail = await _userRepository.ExistsWithEmail(command.Email, cancellationToken);
        if (withEmail)
            throw new EntityExistsException("User", "email");
        var withNickname = await _userRepository.ExistsWithNickname(command.Nickname, cancellationToken);
        if (withNickname)
            throw new EntityExistsException("User", "nickname");

        var passwordHash = _hasher.Hash(command.Password);
        var user = new Domain.Models.User.User(
            command.Name,
            command.Email,
            command.Nickname,
            passwordHash
        );
        await _userRepository.Save(user, cancellationToken);
        return Unit.Value;
    }
}