using Domain.Models.User;
using MediatR;

namespace Application.Queries.User.GetUser;

public record GetUserQuery(Guid UserId) : IRequest<ExpandedUserMiniature>;