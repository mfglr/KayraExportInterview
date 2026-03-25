using Shared.Exceptions;

namespace AuthService.Application.Exceptions
{
    internal class UserNotFoundException() : NotFoundException("User not found!");
}