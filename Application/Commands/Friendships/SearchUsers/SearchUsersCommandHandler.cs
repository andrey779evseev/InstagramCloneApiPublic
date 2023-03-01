using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.Common;
using Domain.Models.User;
using MediatR;

namespace Application.Commands.Friendships.SearchUsers;

public class SearchUsersCommandHandler : IRequestHandler<SearchUsersCommand, Page<ExtendedUserMiniature>>
{
    private readonly ILogger _logger;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;

    public SearchUsersCommandHandler(
        IUserAccessor userAccessor,
        IUserRepository userRepository,
        ILogger logger
    )
    {
        _userAccessor = userAccessor;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Page<ExtendedUserMiniature>> Handle(SearchUsersCommand command,
        CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var take = command.Take ?? 15;

        var exclude = new List<Guid> {user.Id};

        var page = await _userRepository.FindExtendedWithExcludeByPages(
            exclude,
            command.Search,
            command.Cursor,
            take,
            cancellationToken
        );

        return page;
    }
}