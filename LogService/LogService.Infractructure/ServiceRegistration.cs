using LogService.Infractructure.ElasticSearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogService.Infractructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrstructure(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddElacticSearch(configuration);
    }
}
