using Domain.Models.RefreshToken;
using Domain.Models.User;
using Domain.Utils.Tokens;

namespace Domain.Interfaces.Utils.Tokens;

public interface IAuthenticateService
{
    public Task<AuthenticateResponse> Authenticate(
        User user,
        CancellationToken cancellationToken,
        RefreshToken? validRefreshToken = null
    );
}