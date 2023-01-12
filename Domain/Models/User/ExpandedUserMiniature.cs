namespace Domain.Models.User;

/// <summary>
/// Expanded user miniature with following properties: 
/// </summary>
public record ExpandedUserMiniature(
    Guid Id,
    string Nickname,
    string? Avatar,
    string Name,
    string Description,
    bool Followed
);