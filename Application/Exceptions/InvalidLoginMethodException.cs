using Newtonsoft.Json;

namespace Application.Exceptions;

public class InvalidLoginMethodException : Exception
{
    public InvalidLoginMethodException()
        : base()
    {
    }

    public InvalidLoginMethodException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public InvalidLoginMethodException(object obj)
        : base(JsonConvert.SerializeObject(obj))
    {
    }
}