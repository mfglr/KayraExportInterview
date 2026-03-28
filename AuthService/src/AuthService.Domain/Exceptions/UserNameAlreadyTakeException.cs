using Shared.Exceptions;

namespace AuthService.Domain.Exceptions
{
    internal class UserNameAlreadyTakeException() : ValidationException("The user name has already been used!");
}