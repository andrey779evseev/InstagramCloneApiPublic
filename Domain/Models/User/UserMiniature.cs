namespace Domain.Models.User;

/// <summary>
/// User miniature with following properties: Id, Nickname, Avatar
/// </summary>
public record UserMiniature(
    Guid Id,
    string Nickname,
    string? Avatar
);