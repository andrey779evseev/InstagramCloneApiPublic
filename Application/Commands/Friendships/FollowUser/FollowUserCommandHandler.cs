using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using MediatR;

namespace Application.Commands.Friendships.FollowUser;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand>
{
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;

    public FollowUserCommandHandler(
        IUserAccessor userAccessor,
        IUserRepository userRepository
    )
    {
        _userAccessor = userAccessor;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(FollowUserCommand command, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var alreadyFollowed = user.Following.Contains(command.UserId);
        if (alreadyFollowed)
            throw new EntityExistsException("You already follow this user");

        var followingUser = await _userRepository.OneById(command.UserId, cancellationToken);
        if (followingUser == null)
            throw new UserDoesNotExistException("id");

        user.Following.Add(followingUser.Id);
        followingUser.Followers.Add(user.Id);

        await _userRepository.Save(user, cancellationToken);
        await _userRepository.Save(followingUser, cancellationToken);

        return Unit.Value;
    }
}