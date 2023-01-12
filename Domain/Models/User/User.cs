using Newtonsoft.Json;

namespace Domain.Models.User;

/// <summary>
/// full model of user
/// </summary>
public class User
{
    public User(
        string name,
        string email,
        string nickname,
        string? hashedPassword = null,
        string? googleId = null,
        string description = "",
        string gender = ""
    )
    {
        Id = Guid.NewGuid();
        GoogleId = googleId;
        Name = name;
        Email = email;
        HashedPassword = hashedPassword;
        Nickname = nickname;
        Description = description;
        Gender = gender;
        Followers = new List<Guid>();
        Following = new List<Guid>();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public Guid Id { get; }
    public string? GoogleId { get; }
    public string Name { get; private set; }
    public string Nickname { get; private set; }
    public string Email { get; private set; }
    [JsonIgnore] public string? HashedPassword { get; private set; }
    public string Description { get; private set; }
    public string Gender { get; private set; }
    public string? Avatar { get; private set; }
    [JsonIgnore] public List<Guid> Followers { get; }
    [JsonIgnore] public List<Guid> Following { get; }
    [JsonIgnore] public DateTime CreatedAt { get; }
    [JsonIgnore] public DateTime UpdatedAt { get; private set; }


    //for relationships
    [JsonIgnore] public virtual IEnumerable<RefreshToken.RefreshToken> RefreshTokens { get; } = null!;
    [JsonIgnore] public virtual IEnumerable<Post.Post> Posts { get; } = null!;
    [JsonIgnore] public virtual IEnumerable<Like.Like> Likes { get; } = null!;
    [JsonIgnore] public virtual IEnumerable<Comment.Comment> Comments { get; } = null!;
    [JsonIgnore] public virtual IEnumerable<EndpointLog.EndpointLog> EndpointLogs { get; } = null!;

    public void SetAvatar(string avatar)
    {
        Avatar = avatar;
        UpdateDate();
    }

    public void Update(
        string name,
        string nickname,
        string email,
        string? description,
        string? gender
    )
    {
        Name = name;
        Nickname = nickname;
        Email = email;
        Description = description ?? string.Empty;
        Gender = gender ?? string.Empty;
        UpdateDate();
    }

    public void UpdatePassword(string hash)
    {
        HashedPassword = hash;
        UpdateDate();
    }

    public void UpdateDate()
    {
        UpdatedAt = DateTime.Now;
    }

    //mappers
    public UserMiniature ToMiniature()
    {
        return new UserMiniature(Id, Nickname, Avatar);
    }

    public ExtendedUserMiniature ToExtendedMiniature()
    {
        return new ExtendedUserMiniature(Id, Nickname, Avatar, Name);
    }
}