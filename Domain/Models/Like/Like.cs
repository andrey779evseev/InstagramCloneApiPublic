using Newtonsoft.Json;

namespace Domain.Models.Like;

public class Like
{
    public Like(Guid userId, Guid postId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        PostId = postId;
        LikedAt = DateTime.Now;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid PostId { get; }
    public DateTime LikedAt { get; }

    //for relationships
    [JsonIgnore] public User.User User { get; } = null!;
    [JsonIgnore] public Post.Post Post { get; } = null!;

    public LikeDetail ToDetail()
    {
        return new LikeDetail(
            Id,
            PostId,
            User.ToExtendedMiniature()
        );
    }
}