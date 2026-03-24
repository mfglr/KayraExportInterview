namespace AuthService.Application
{
    public record TokenResponse(string AccessToken, string RefreshToken);
}
