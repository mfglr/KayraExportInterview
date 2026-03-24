using AuthService.Application;

namespace AuthService.Infrastructure
{
    internal record TokenOptions(
        string Audience,
        string Issuer,
        int AccessTokenExpiration,
        int RefreshTokenExpiration,
        string SecurityKey
    ) : ITokenOptions;
}
