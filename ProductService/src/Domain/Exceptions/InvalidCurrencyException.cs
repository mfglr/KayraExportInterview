using Shared.Exceptions;

namespace Domain.Exceptions
{
    public class InvalidCurrencyException() : ValidationException("Invalid currency.");
}