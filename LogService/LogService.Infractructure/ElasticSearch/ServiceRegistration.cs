using Elastic.Clients.Elasticsearch;
using LogService.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogService.Infractructure.ElasticSearch
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddElacticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var option = configuration.GetSection(nameof(ElasticSearchOptions))!;
            var clientSettings = new ElasticsearchClientSettings(new Uri(option["Host"]!));

            return services
                .AddSingleton(new ElasticsearchClient(clientSettings))
                .AddScoped<ILogRepository, LogRepository>();
        }
            
    }
}
