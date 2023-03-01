using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Queries.User.CheckNickname;

public class CheckNicknameQueryHandler : IRequestHandler<CheckNicknameQuery, bool>
{
    private readonly IUserRepository _userRepository;

    public CheckNicknameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(CheckNicknameQuery query, CancellationToken cancellationToken)
    {
        var exist = await _userRepository.ExistsWithNickname(query.Nickname, cancellationToken);
        return !exist;
    }
}