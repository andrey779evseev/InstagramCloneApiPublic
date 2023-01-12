using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Utils.Logger;
using Domain.Models.User;
using MediatR;

namespace Application.Queries.Friendships.GetSuggestions;

public class GetSuggestionsQueryHandler : IRequestHandler<GetSuggestionsQuery, List<ExtendedUserMiniature>>
{
    private readonly ILogger _logger;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;

    public GetSuggestionsQueryHandler(
        IUserAccessor userAccessor,
        IUserRepository userRepository,
        ILogger logger
    )
    {
        _userAccessor = userAccessor;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<List<ExtendedUserMiniature>> Handle(GetSuggestionsQuery query,
        CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var exclude = user.Following.ToList();
        exclude.Add(user.Id);

        var suggestions = await _userRepository.FindExtendedWithExclude(
            exclude,
            query.Take,
            cancellationToken
        );

        return suggestions;
    }
}