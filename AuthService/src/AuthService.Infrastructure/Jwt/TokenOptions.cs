using AuthService.Application;

namespace AuthService.Infrastructure.Jwt
{
    internal record TokenOptions(
        List<string> Audience,
        string Issuer,
        int AccessTokenValidtyPeriod,
        int RefreshTokenValidtyPeriod,
        string SecurityKey
    ) : ITokenOptions;
}
