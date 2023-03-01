using Application.Exceptions;
using Domain.Interfaces.Accessors;
using MediatR;

namespace Application.Queries.User.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Domain.Models.User.User>
{
    private readonly IUserAccessor _userAccessor;

    public GetCurrentUserQueryHandler(
        IUserAccessor userAccessor
    )
    {
        _userAccessor = userAccessor;
    }

    public Task<Domain.Models.User.User> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();

        if (user == null)
            throw new UserDoesNotExistException();

        return Task.FromResult(user);
    }
}