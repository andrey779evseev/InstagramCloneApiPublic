using Newtonsoft.Json;

namespace Domain.Models.Post;

public class Post
{
    public Post(Guid authorId, string description, string photo)
    {
        Id = Guid.NewGuid();
        AuthorId = authorId;
        Description = description;
        Photo = photo;
        PostedAt = DateTime.Now;
    }

    public Guid Id { get; }
    [JsonIgnore] public Guid AuthorId { get; }
    public string Description { get; }
    public string Photo { get; }
    public DateTime PostedAt { get; }

    //for relationships
    [JsonIgnore] public User.User User { get; } = null!;
    [JsonIgnore] public IEnumerable<Like.Like> Likes { get; } = null!;
    [JsonIgnore] public IEnumerable<Comment.Comment> Comments { get; } = null!;

    //mappers
    public PostMiniature ToMiniature(int likesCount, int commentsCount)
    {
        return new PostMiniature(
            Id,
            Photo,
            PostedAt,
            likesCount,
            commentsCount
        );
    }
}