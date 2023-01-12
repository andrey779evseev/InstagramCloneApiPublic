using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Models.User;
using MediatR;

namespace Application.Queries.User.GetStats;

public class GetStatsQueryHandler : IRequestHandler<GetStatsQuery, UserStats>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public GetStatsQueryHandler(
        IUserRepository userRepository,
        IPostRepository postRepository
    )
    {
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    public async Task<UserStats> Handle(GetStatsQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OneById(query.UserId, cancellationToken);
        if (user == null)
            throw new UserDoesNotExistException("id");

        var postsCount = await _postRepository.CountByUser(user, cancellationToken);

        var stats = new UserStats(
            user.Followers.Count,
            user.Following.Count,
            postsCount
        );
        return stats;
    }
}