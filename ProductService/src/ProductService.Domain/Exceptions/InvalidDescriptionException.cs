using Shared.Exceptions;

namespace ProductService.Domain.Exceptions
{
    public class InvalidDescriptionException(string message) : ValidationException(message);
}
