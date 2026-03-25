using Shared.Exceptions;

namespace AuthService.Domain.Exceptions
{
    internal class InvalidPasswordException(string message) : ValidationException(message);
}