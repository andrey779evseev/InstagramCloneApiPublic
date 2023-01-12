using MediatR;

namespace Application.Queries.Post.GetPost;

public record GetPostQuery(Guid PostId) : IRequest<Domain.Models.Post.Post>;