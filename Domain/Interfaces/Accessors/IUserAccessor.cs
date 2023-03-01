using Domain.Models.User;

namespace Domain.Interfaces.Accessors;

public interface IUserAccessor
{
    public User? Get();
}