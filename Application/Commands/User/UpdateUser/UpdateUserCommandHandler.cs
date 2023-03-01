using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using MediatR;

namespace Application.Commands.User.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Domain.Models.User.User>
{
    private readonly ILogger _logger;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(
        IUserAccessor userAccessor,
        IUserRepository userRepository,
        ILogger logger
    )
    {
        _userAccessor = userAccessor;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Domain.Models.User.User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var existWithNickname = await _userRepository.ExistsWithNickname(command.Nickname, cancellationToken);
        if (existWithNickname)
            throw new EntityExistsException("User with this nickname already exist");

        user.Update(
            command.Name,
            command.Nickname,
            command.Email,
            command.Description,
            command.Gender
        );

        await _userRepository.Save(user, cancellationToken);

        return user;
    }
}