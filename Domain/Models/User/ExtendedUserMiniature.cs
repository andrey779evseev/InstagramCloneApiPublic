namespace Domain.Models.User;

/// <summary>
/// Extended user miniature with following properties: Id, Nickname, Name, Avatar, Name
/// </summary>
public record ExtendedUserMiniature(
    Guid Id,
    string Nickname,
    string? Avatar,
    string Name
);