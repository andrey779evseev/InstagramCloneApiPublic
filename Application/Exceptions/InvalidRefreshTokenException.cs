namespace Application.Exceptions;

public class InvalidRefreshTokenException : Exception
{
    public InvalidRefreshTokenException()
        : base("This refresh token isn't valid")
    {
    }

    public InvalidRefreshTokenException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public InvalidRefreshTokenException(string message)
        : base(message)
    {
    }

    public InvalidRefreshTokenException(string entity, string field)
        : base("Invalid refresh token")
    {
    }
}