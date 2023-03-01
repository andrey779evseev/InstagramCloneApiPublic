using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.User;
using MediatR;

namespace Application.Queries.User.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ExpandedUserMiniature>
{
    private readonly ILogger _logger;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(
        IUserAccessor userAccessor,
        IUserRepository userRepository,
        ILogger logger
    )
    {
        _userAccessor = userAccessor;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ExpandedUserMiniature> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var currentUser = _userAccessor.Get();
        if (currentUser == null)
            throw new UserDoesNotExistException();

        var user = await _userRepository.OneById(query.UserId, cancellationToken);
        if (user == null)
            throw new UserDoesNotExistException("id");

        var followed = user.Following.Contains(currentUser.Id);

        return new ExpandedUserMiniature(
            user.Id,
            user.Nickname,
            user.Avatar,
            user.Name,
            user.Description,
            followed
        );
    }
}