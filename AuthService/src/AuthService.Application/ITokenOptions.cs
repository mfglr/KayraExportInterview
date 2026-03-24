namespace AuthService.Application
{
    public interface ITokenOptions
    {
        string Audience { get; }
        string Issuer { get; }
        int AccessTokenExpiration { get; }
        int RefreshTokenExpiration { get; }
        string SecurityKey { get; }
    }
}
