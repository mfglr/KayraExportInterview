namespace Gateway.Api.Auth
{
    internal record AuthOptions(
        string Issuer,
        string Audience,
        string SecurityKey
    );
}
