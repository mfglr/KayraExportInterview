using Shared.Exceptions;

namespace AuthService.Domain.Exceptions
{
    public class InvalidUserNameException(string message) : ValidationException(message);
}
