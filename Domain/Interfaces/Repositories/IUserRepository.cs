using Domain.Models.Common;
using Domain.Models.User;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    public Task Save(User user, CancellationToken cancellationToken);
    public Task<bool> ExistsWithEmail(string email, CancellationToken cancellationToken);
    public Task<bool> ExistsWithNickname(string nickname, CancellationToken cancellationToken);
    public Task<bool> ExistsWithId(Guid id, CancellationToken cancellationToken);
    public Task<User?> OneByEmail(string email, CancellationToken cancellationToken);
    public Task<User?> OneByGoogleId(string googleId, CancellationToken cancellationToken);
    public Task<User?> OneById(Guid id, CancellationToken cancellationToken);
    public Task<List<UserMiniature>> FindMiniaturesByIds(List<Guid> ids, CancellationToken cancellationToken);
    public Task<UserMiniature?> OneMiniatureById(Guid id, CancellationToken cancellationToken);

    public Task<List<ExtendedUserMiniature>> FindExtendedWithExclude(List<Guid> exclude, int take,
        CancellationToken cancellationToken);

    public Task<Page<ExtendedUserMiniature>> FindExtendedWithExcludeByPages(List<Guid> exclude, string? search,
        Guid? cursor, int take,
        CancellationToken cancellationToken);
}