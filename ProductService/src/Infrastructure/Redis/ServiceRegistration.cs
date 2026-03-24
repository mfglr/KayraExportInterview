using Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Redis
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(ConnectionMultiplexer.Connect(configuration["Redis:Host"]!))
                .AddScoped(sp =>
                {
                    var multiplexer = sp.GetRequiredService<ConnectionMultiplexer>();
                    return multiplexer.GetDatabase();
                })
                .AddScoped<IProductCacheService, RedisProductCacheService>()
                .AddScoped<IProductCacheService, RedisProductCacheService>();
    }
}
