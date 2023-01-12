using Domain.Models.RefreshToken;

namespace Domain.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    public Task Save(RefreshToken token, CancellationToken cancellationToken);
    public Task<RefreshToken?> OneByToken(string token, CancellationToken cancellationToken);
}