namespace AuthService.Domain
{
    public class InvalidOrExpiredRefreshToken() : Exception("The refresh token is invalid or expired");
}