using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.User;
using MediatR;

namespace Application.Queries.Friendships.GetFollowing;

public class GetFollowingQueryHandler : IRequestHandler<GetFollowingQuery, List<UserMiniature>>
{
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;

    public GetFollowingQueryHandler(
        IUserRepository userRepository,
        ILogger logger
    )
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<List<UserMiniature>> Handle(GetFollowingQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OneById(query.UserId, cancellationToken);
        if (user == null)
            throw new UserDoesNotExistException("id");

        var following = await _userRepository.FindMiniaturesByIds(user.Following, cancellationToken);

        return following;
    }
}