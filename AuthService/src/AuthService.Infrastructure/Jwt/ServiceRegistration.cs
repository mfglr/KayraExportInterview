using AuthService.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.Jwt
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>()!;
            return services
                .AddSingleton<ITokenOptions>(options)
                .AddScoped<IAccessTokenGenerator, AccessTokenGenerator>();
        }

    }
}
