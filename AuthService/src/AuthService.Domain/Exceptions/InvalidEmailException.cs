using Shared.Exceptions;

namespace AuthService.Domain.Exceptions
{
    internal class InvalidEmailException() : ValidationException("Email is not valid!");
}