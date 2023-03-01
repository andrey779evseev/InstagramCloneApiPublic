namespace Domain.Interfaces.Utils.PasswordHasher;

public interface IPasswordHasher
{
    string Hash(string password);
  
    bool Check(string hash, string password);
}