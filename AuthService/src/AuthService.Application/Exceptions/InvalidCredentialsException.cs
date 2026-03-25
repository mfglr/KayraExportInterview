using Shared.Exceptions;

namespace AuthService.Application.Exceptions
{
    internal class InvalidCredentialsException() : ValidationException("Invalid credentials.");
}