using Shared.Exceptions;

namespace Domain.Exceptions
{
    public class InvalidDescriptionException(string message) : ValidationException(message);
}
