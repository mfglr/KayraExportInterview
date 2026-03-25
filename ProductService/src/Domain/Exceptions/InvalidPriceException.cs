using Shared.Exceptions;

namespace Domain.Exceptions
{
    public class InvalidPriceException() : ValidationException("Price must be greater than zero.");
}