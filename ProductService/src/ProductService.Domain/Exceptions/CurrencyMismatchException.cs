using Shared.Exceptions;

namespace ProductService.Domain.Exceptions
{
    public class CurrencyMismatchException() : ValidationException("Currencies must match.");
}