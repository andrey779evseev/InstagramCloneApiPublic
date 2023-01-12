namespace Application.Exceptions;

public class UnexpectedErrorException : Exception
{
    public UnexpectedErrorException()
        : base()
    {
    }

    public UnexpectedErrorException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public UnexpectedErrorException(string entity, string field)
        : base("Unexpected error happens")
    {
    }
}