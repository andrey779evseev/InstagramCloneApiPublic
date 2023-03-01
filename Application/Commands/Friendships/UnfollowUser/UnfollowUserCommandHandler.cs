using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using MediatR;

namespace Application.Commands.Friendships.UnfollowUser;

public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand>
{
    private readonly ILogger _logger;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;

    public UnfollowUserCommandHandler(
        IUserAccessor userAccessor,
        IUserRepository userRepository,
        ILogger logger
    )
    {
        _userAccessor = userAccessor;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UnfollowUserCommand command, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var followed = user.Following.Contains(command.UserId);
        if (!followed)
            throw new EntityExistsException("You doesn't follow this user");

        var followingUser = await _userRepository.OneById(command.UserId, cancellationToken);
        if (followingUser == null)
            throw new UserDoesNotExistException("id");

        user.Following.Remove(followingUser.Id);
        followingUser.Followers.Remove(user.Id);

        await _userRepository.Save(user, cancellationToken);
        await _userRepository.Save(followingUser, cancellationToken);

        return Unit.Value;
    }
}