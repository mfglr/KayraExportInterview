using Shared.Exceptions;

namespace AuthService.Domain.Exceptions
{
    public class InvalidOrExpiredRefreshTokenException() : ValidationException("The refresh token is invalid or expired");
}