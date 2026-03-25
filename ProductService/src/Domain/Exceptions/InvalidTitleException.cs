using Shared.Exceptions;

namespace Domain.Exceptions
{
    public class InvalidTitleException(string message) : ValidationException(message);
}
