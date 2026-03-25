using Shared.Exceptions;

namespace AuthService.Domain.Exceptions
{
    internal class EmailAlreadyTakenException() : ValidationException("Email has been already taken!");
}