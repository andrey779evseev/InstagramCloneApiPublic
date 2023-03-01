namespace Application.Exceptions;

public class InvalidUserCredentialsException : Exception
{
    public InvalidUserCredentialsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public InvalidUserCredentialsException()
        : base($"Provided email or password is incorrect.")
    {
    }
}