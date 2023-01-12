using Domain.Models.Comment;

namespace Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    public Task<List<CommentDetail>> FindByPost(Guid postId, CancellationToken cancellationToken);
    public Task Save(Comment comment, CancellationToken cancellationToken);
    public Task<int> CountByPost(Guid postId, CancellationToken cancellationToken);
    public Task<CommentMiniature?> OneByPost(Guid postId, CancellationToken cancellationToken);
}