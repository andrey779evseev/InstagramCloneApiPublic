using Domain.Models.User;
using MediatR;

namespace Application.Queries.Friendships.GetSuggestions;

public record GetSuggestionsQuery(int Take) : IRequest<List<ExtendedUserMiniature>>;