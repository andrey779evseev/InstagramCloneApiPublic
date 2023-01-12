using Domain.Models.User;
using Domain.Utils.Tokens;

namespace Domain.Interfaces.Utils.Tokens;

public interface ITokenService
{
    GenerateTokenResponse Generate(User user);
}