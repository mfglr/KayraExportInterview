using Shared.Exceptions;

namespace ProductService.Domain.Exceptions
{
    public class InvalidPriceException() : ValidationException("Price must be greater than zero.");
}