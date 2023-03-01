namespace Application.Exceptions;

public class TokenAlreadyRevokedException : Exception
{
    public TokenAlreadyRevokedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public TokenAlreadyRevokedException()
        : base("The token you provided has already been revoked")
    {
    }
}