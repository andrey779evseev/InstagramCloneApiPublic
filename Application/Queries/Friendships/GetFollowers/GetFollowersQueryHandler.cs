using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.User;
using MediatR;

namespace Application.Queries.Friendships.GetFollowers;

public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQuery, List<UserMiniature>>
{
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;

    public GetFollowersQueryHandler(
        IUserRepository userRepository,
        ILogger logger
    )
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<List<UserMiniature>> Handle(GetFollowersQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OneById(query.UserId, cancellationToken);
        if (user == null)
            throw new UserDoesNotExistException("id");

        var followers = await _userRepository.FindMiniaturesByIds(user.Followers, cancellationToken);

        return followers;
    }
}