using Domain.Models.User;
using MediatR;

namespace Application.Queries.Post.GetAuthor;

public record GetAuthorQuery(Guid PostId) : IRequest<UserMiniature>;