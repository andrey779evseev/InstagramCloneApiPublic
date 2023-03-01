using MediatR;

namespace Application.Queries.User.CheckNickname;

public record CheckNicknameQuery(string Nickname) : IRequest<bool>;