namespace AuthService.Application
{
    public interface ITokenOptions
    {
        string Audience { get; }
        string Issuer { get; }
        int AccessTokenValidtyPeriod { get; }
        int RefreshTokenValidtyPeriod { get; }
        string SecurityKey { get; }
    }
}
