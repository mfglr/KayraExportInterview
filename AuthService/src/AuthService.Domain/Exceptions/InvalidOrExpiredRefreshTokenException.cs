namespace AuthService.Domain.Exceptions
{
    public class InvalidOrExpiredRefreshTokenException() : Exception("The refresh token is invalid or expired");
}