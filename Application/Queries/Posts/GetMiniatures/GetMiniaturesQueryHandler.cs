using Application.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Models.Post;
using MediatR;

namespace Application.Queries.Posts.GetMiniatures;

public class GetMiniaturesQueryHandler : IRequestHandler<GetMiniaturesQuery, List<PostMiniature>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public GetMiniaturesQueryHandler(
        IPostRepository postRepository,
        IUserRepository userRepository
    )
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<List<PostMiniature>> Handle(GetMiniaturesQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OneById(query.UserId, cancellationToken);
        if (user == null)
            throw new UserDoesNotExistException("id");

        var miniatures = await _postRepository.FindMiniaturesByUser(
            user,
            query.Cursor,
            query.Take,
            cancellationToken
        );

        return miniatures;
    }
}