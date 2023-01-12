using Domain.Interfaces.Repositories;
using Domain.Models.Common;
using Domain.Models.User;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SupabaseDbContext _ctx;

    public UserRepository(SupabaseDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task Save(User user, CancellationToken cancellationToken)
    {
        if (_ctx.Users.Contains(user))
        {
            user.UpdateDate();
            _ctx.Users.Update(user);
        }
        else
            await _ctx.Users.AddAsync(user, cancellationToken);

        await _ctx.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithEmail(string email, CancellationToken cancellationToken)
    {
        return await _ctx.Users.AnyAsync(user => user.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsWithNickname(string nickname, CancellationToken cancellationToken)
    {
        return await _ctx.Users.AnyAsync(user => user.Nickname == nickname, cancellationToken);
    }

    public async Task<bool> ExistsWithId(Guid id, CancellationToken cancellationToken)
    {
        return await _ctx.Users.AnyAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<User?> OneByEmail(string email, CancellationToken cancellationToken)
    {
        return await _ctx.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public async Task<User?> OneByGoogleId(string googleId, CancellationToken cancellationToken)
    {
        return await _ctx.Users.FirstOrDefaultAsync(user => user.GoogleId == googleId, cancellationToken);
    }

    public async Task<User?> OneById(Guid id, CancellationToken cancellationToken)
    {
        return await _ctx.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<List<UserMiniature>> FindMiniaturesByIds(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await _ctx.Users
            .Where(user => ids.Contains(user.Id))
            .Select(user => user.ToMiniature())
            .ToListAsync(cancellationToken);
    }

    public async Task<UserMiniature?> OneMiniatureById(Guid id, CancellationToken cancellationToken)
    {
        return await _ctx.Users
            .Where(user => user.Id == id)
            .Select(user => user.ToMiniature())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ExtendedUserMiniature>> FindExtendedWithExclude(
        List<Guid> exclude,
        int take,
        CancellationToken cancellationToken
    )
    {
        return await _ctx
            .Users
            .Where(user => !exclude.Contains(user.Id))
            .Take(take)
            .Select(user => user.ToExtendedMiniature())
            .ToListAsync(cancellationToken);
    }

    public async Task<Page<ExtendedUserMiniature>> FindExtendedWithExcludeByPages(
        List<Guid> exclude,
        string? search,
        Guid? cursor,
        int take,
        CancellationToken cancellationToken
    )
    {
        var searchString = search?.ToLower().Trim();
        var users = await _ctx
            .Users
            .Where(user => !exclude.Contains(user.Id))
            .Where(user =>
                searchString == null ||
                user.Nickname.ToLower().Contains(searchString) ||
                user.Name.ToLower().Contains(searchString) ||
                user.Email.ToLower().Contains(searchString)
            )
            .OrderByDescending(user => user.Id)
            // ReSharper disable once StringCompareToIsCultureSpecific
            .Where(user => cursor == null || user.Id.ToString().CompareTo(cursor.ToString()) < 0)
            .Take(take + 1)
            .Select(user => user.ToExtendedMiniature())
            .ToListAsync(cancellationToken);
        var hasNextPage = users.Count > take;
        return new Page<ExtendedUserMiniature>(hasNextPage, users.Take(take).ToList());
    }
}