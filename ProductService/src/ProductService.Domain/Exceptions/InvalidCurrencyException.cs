using Shared.Exceptions;

namespace ProductService.Domain.Exceptions
{
    public class InvalidCurrencyException() : ValidationException("Invalid currency.");
}