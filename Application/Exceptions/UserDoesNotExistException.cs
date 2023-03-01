namespace Application.Exceptions;

public class UserDoesNotExistException : Exception
{
    public UserDoesNotExistException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// for not founded user by id from access token 
    /// </summary>
    public UserDoesNotExistException()
        : base($"User referenced by this token does not exist")
    {
    }

    /// <summary>
    /// for not founded user by some field
    /// </summary>
    public UserDoesNotExistException(string field)
        : base($"User referenced by this {field} does not exist")
    {
    }
}