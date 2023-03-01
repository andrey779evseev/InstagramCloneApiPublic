using Domain.Models.User;
using MediatR;

namespace Application.Queries.User.GetStats;

public record GetStatsQuery(Guid UserId) : IRequest<UserStats>;