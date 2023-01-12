using MediatR;

namespace Application.Queries.User.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<Domain.Models.User.User>;