using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Infrastructure.EfCore;
using ProductService.Infrastructure.Redis;

namespace ProductService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddEfCore(configuration)
                .AddRedis(configuration);
    }
}
