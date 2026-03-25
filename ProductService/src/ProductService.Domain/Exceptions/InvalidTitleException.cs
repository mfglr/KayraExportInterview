using Shared.Exceptions;

namespace ProductService.Domain.Exceptions
{
    public class InvalidTitleException(string message) : ValidationException(message);
}
