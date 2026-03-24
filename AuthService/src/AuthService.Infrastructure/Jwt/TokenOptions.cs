using AuthService.Application;

namespace AuthService.Infrastructure.Jwt
{
    internal record TokenOptions(
        string Audience,
        string Issuer,
        int AccessTokenValidtyPeriod,
        int RefreshTokenValidtyPeriod,
        string SecurityKey
    ) : ITokenOptions;
}
