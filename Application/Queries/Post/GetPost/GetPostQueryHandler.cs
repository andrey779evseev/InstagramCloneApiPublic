using Application.Exceptions;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Queries.Post.GetPost;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, Domain.Models.Post.Post>
{
    private readonly IPostRepository _postRepository;

    public GetPostQueryHandler(
        IPostRepository postRepository
    )
    {
        _postRepository = postRepository;
    }

    public async Task<Domain.Models.Post.Post> Handle(GetPostQuery query, CancellationToken cancellationToken)
    {
        var post = await _postRepository.OneById(query.PostId, cancellationToken);
        if (post == null)
            throw new NotFoundException("Post", "id");

        return post;
    }
}