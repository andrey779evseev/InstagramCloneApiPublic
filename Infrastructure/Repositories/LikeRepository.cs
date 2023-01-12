using Domain.Interfaces.Repositories;
using Domain.Models.Like;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly SupabaseDbContext _ctx;

    public LikeRepository(SupabaseDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Like?> OneByPostAndUser(Guid userId, Guid postId, CancellationToken cancellationToken)
    {
        return await _ctx.Likes
            .FirstOrDefaultAsync(like => like.UserId == userId && like.PostId == postId, cancellationToken);
    }

    public async Task<List<LikeDetail>> FindDetailsByPost(Guid postId, CancellationToken cancellationToken)
    {
        return await _ctx.Likes
            .Where(like => like.PostId == postId)
            .Include(like => like.User)
            .Select(like => like.ToDetail())
            .ToListAsync(cancellationToken);
    }

    public async Task<string?> FirstNicknameByPost(Guid postId, CancellationToken cancellationToken)
    {
        return await _ctx.Likes
            .Where(like => like.PostId == postId)
            .Include(like => like.User)
            .Select(like => like.User.Nickname)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<string>> FirstAvatarsByPost(Guid postId, CancellationToken cancellationToken)
    {
        return await _ctx.Likes
            .Where(like => like.PostId == postId)
            .Include(like => like.User)
            .Where(like => like.User.Avatar != null)
            .Select(like => like.User.Avatar!)
            .Take(3)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountByPost(Guid postId, CancellationToken cancellationToken)
    {
        return await _ctx.Likes
            .Where(like => like.PostId == postId)
            .CountAsync(cancellationToken);
    }

    public async Task Save(Like like, CancellationToken cancellationToken)
    {
        await _ctx.Likes.AddAsync(like, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithPostAndUser(Guid userId, Guid postId, CancellationToken cancellationToken)
    {
        return await _ctx.Likes
            .AnyAsync(like => like.UserId == userId && like.PostId == postId, cancellationToken);
    }

    public async Task Delete(Like like, CancellationToken cancellationToken)
    {
        _ctx.Likes.Remove(like);
        await _ctx.SaveChangesAsync(cancellationToken);
    }
}