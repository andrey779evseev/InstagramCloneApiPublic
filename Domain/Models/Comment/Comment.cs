using Newtonsoft.Json;

namespace Domain.Models.Comment;

public class Comment
{
    public Comment(string text, Guid userId, Guid postId)
    {
        Id = Guid.NewGuid();
        Text = text;
        UserId = userId;
        PostId = postId;
        CommentedAt = DateTime.Now;
    }

    public string Text { get; }
    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid PostId { get; }
    public DateTime CommentedAt { get; }

    //for relationships
    [JsonIgnore] public User.User User { get; } = null!;
    [JsonIgnore] public Post.Post Post { get; } = null!;

    //mappers

    /// <summary>
    /// For using this method Comment must have included User model
    /// </summary>
    public CommentDetail ToDetail()
    {
        return new CommentDetail(
            Id,
            PostId,
            User.ToMiniature(),
            Text,
            CommentedAt
        );
    }

    /// <summary>
    /// For using this method Comment must have included User model
    /// </summary>
    public CommentMiniature ToMiniature()
    {
        return new CommentMiniature(
            Id,
            PostId,
            User.Nickname,
            Text
        );
    }
}