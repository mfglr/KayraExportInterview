using AuthService.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Domain
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services) =>
            services
                .AddScoped<UserCreatorDomainService>()
                .AddScoped<UserNameUpdaterDomainService>();
    }
}
