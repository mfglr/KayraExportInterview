using Shared.Exceptions;

namespace AuthService.Domain.DomainServices
{
    internal class UserNameAlreadyTakeException() : ValidationException("The user name has already been used!");
}