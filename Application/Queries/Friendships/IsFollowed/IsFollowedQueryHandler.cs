using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using MediatR;

namespace Application.Queries.Friendships.IsFollowed;

public class IsFollowedQueryHandler : IRequestHandler<IsFollowedQuery, bool>
{
    private readonly ILogger _logger;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;

    public IsFollowedQueryHandler(
        IUserAccessor userAccessor,
        IUserRepository userRepository,
        ILogger logger
    )
    {
        _userAccessor = userAccessor;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(IsFollowedQuery query, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var isExist = await _userRepository.ExistsWithId(query.UserId, cancellationToken);
        if (!isExist)
            throw new UserDoesNotExistException("id");

        return user.Following.Contains(query.UserId);
    }
}