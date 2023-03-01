namespace Application.Exceptions;

public class EntityExistsException : Exception
{
    public EntityExistsException()
        : base()
    {
    }

    public EntityExistsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public EntityExistsException(string message)
        : base(message)
    {
    }

    public EntityExistsException(string entity, string field)
        : base($"${entity} with provided ${field} already exists")
    {
    }
}