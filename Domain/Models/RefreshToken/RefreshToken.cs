namespace Domain.Models.RefreshToken;

public class RefreshToken
{
    public RefreshToken(string token, Guid userId, DateTime expiresAt)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        CreatedAt = DateTime.Now;
        Revoked = false;
    }

    public Guid UserId { get; }
    public string Token { get; }
    public DateTime ExpiresAt { get; }
    public DateTime CreatedAt { get; }

    public bool Revoked { get; private set; }

    //for relationships
    public User.User User { get; } = null!;

    public void Revoke()
    {
        Revoked = true;
    }
}