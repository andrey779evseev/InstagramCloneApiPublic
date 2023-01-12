using Domain.Interfaces.Repositories;
using Domain.Models.Comment;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly SupabaseDbContext _ctx;

    public CommentRepository(
        SupabaseDbContext ctx
    )
    {
        _ctx = ctx;
    }

    public async Task<List<CommentDetail>> FindByPost(Guid postId, CancellationToken cancellationToken)
    {
        return await _ctx.Comments
            .Where(comment => comment.PostId == postId)
            .Include(comment => comment.User)
            .Select(comment => comment.ToDetail())
            .ToListAsync(cancellationToken);
    }

    public async Task Save(Comment comment, CancellationToken cancellationToken)
    {
        await _ctx.Comments.AddAsync(comment, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CountByPost(Guid postId, CancellationToken cancellationToken)
    {
        return await _ctx.Comments.CountAsync(comment => comment.PostId == postId, cancellationToken);
    }

    public async Task<CommentMiniature?> OneByPost(Guid postId, CancellationToken cancellationToken)
    {
        return await _ctx.Comments
            .Where(comment => comment.PostId == postId)
            .OrderByDescending(comment => comment.CommentedAt)
            .Include(comment => comment.User)
            .Select(comment => comment.ToMiniature())
            .FirstOrDefaultAsync(cancellationToken);
    }
}