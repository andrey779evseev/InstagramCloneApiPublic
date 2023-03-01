using Domain.Models.Post;
using Domain.Models.User;

namespace Domain.Interfaces.Repositories;

public interface IPostRepository
{
    public Task<int> CountByUser(User user, CancellationToken cancellationToken);

    public Task<List<PostMiniature>> FindMiniaturesByUser(User user, DateTime? cursor, int limit,
        CancellationToken cancellationToken);

    public Task Save(Post post, CancellationToken cancellationToken);
    public Task<Post?> OneById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Special method for getting feed posts
    /// </summary>
    public Task<List<Post>> FindForFeed(List<Guid> users, DateTime? cursor, int take,
        CancellationToken cancellationToken);

    public Task<bool> ExistsWithId(Guid postId, CancellationToken cancellationToken);
}