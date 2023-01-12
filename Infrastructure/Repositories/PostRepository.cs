using Domain.Interfaces.Repositories;
using Domain.Models.Post;
using Domain.Models.User;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly SupabaseDbContext _ctx;

    public PostRepository(SupabaseDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<int> CountByUser(User user, CancellationToken cancellationToken)
    {
        return await _ctx
            .Posts
            .CountAsync(post => post.AuthorId == user.Id, cancellationToken);
    }

    public async Task<List<PostMiniature>> FindMiniaturesByUser(User user, DateTime? cursor, int take,
        CancellationToken cancellationToken)
    {
        return await _ctx
            .Posts
            .Where(post => post.AuthorId == user.Id)
            .OrderByDescending(post => post.PostedAt)
            .Where(post => cursor == null || DateTime.Compare(post.PostedAt, (DateTime) cursor) < 0)
            .Take(take)
            .Select(post => post.ToMiniature(post.Likes.Count(), post.Comments.Count()))
            .ToListAsync(cancellationToken);
    }

    public async Task Save(Post post, CancellationToken cancellationToken)
    {
        if (_ctx.Posts.Contains(post))
            _ctx.Posts.Update(post);
        else
            await _ctx.Posts.AddAsync(post, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);
    }

    public async Task<Post?> OneById(Guid id, CancellationToken cancellationToken)
    {
        return await _ctx.Posts
            .FirstOrDefaultAsync(post => post.Id == id, cancellationToken);
    }

    public async Task<List<Post>> FindForFeed(List<Guid> users, DateTime? cursor, int take,
        CancellationToken cancellationToken)
    {
        return await _ctx
            .Posts
            .Where(post => users.Contains(post.AuthorId))
            .OrderByDescending(post => post.PostedAt)
            .Where(post => cursor == null || DateTime.Compare(post.PostedAt, (DateTime) cursor) < 0)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithId(Guid postId, CancellationToken cancellationToken)
    {
        return await _ctx.Posts.AnyAsync(post => post.Id == postId, cancellationToken);
    }
}