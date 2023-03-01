using Newtonsoft.Json;

namespace Application.Exceptions;

public class ValidationRequestException : Exception
{
    public ValidationRequestException()
        : base()
    {
    }

    public ValidationRequestException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ValidationRequestException(IEnumerable<string> errors)
        : base(JsonConvert.SerializeObject(errors))
    {
    }

    public ValidationRequestException(string error)
        : base(error)
    {
    }
}