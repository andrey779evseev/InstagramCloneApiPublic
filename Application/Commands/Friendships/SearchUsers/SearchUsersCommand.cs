using Domain.Models.Common;
using Domain.Models.User;
using MediatR;

namespace Application.Commands.Friendships.SearchUsers;

public record SearchUsersCommand(string? Search, Guid? Cursor, int? Take) : IRequest<Page<ExtendedUserMiniature>>;