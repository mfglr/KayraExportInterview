using Elastic.Clients.Elasticsearch;
using LogService.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace LogService.Infractructure.ElasticSearch
{
    public static class MappingConfigurator
    {
        public static async Task ConfigureAsync(IServiceProvider serviceProvider) {
            using var scope = serviceProvider.CreateScope();
            var client = scope.ServiceProvider.GetRequiredService<ElasticsearchClient>();

            var response = await client.PingAsync();
            while (!response.IsSuccess())
            {
                await Task.Delay(1000);
                response = await client.PingAsync();
            };

            await client.Indices
                .CreateAsync<Log>(
                    index => index
                        .Index(IndexNameProvider.IndexName)
                        .Mappings(mappings => mappings
                            .Properties(
                                properties => properties
                                    .Keyword(x => x.ServiceName)
                                    .Date(x => x.Timestamp)
                                    .Keyword(x => x.Level)
                                    .Text(x => x.MessageTemplate)
                                    .Keyword(x => x.TraceId)
                                    .Keyword(x => x.RequestPaths)
                                    .Object(
                                        x => x.Exception,
                                        obj => obj.Properties(
                                            p => p
                                                .Text("Message")
                                                .Text("StackTrace")
                                                .Object("InnerException",i => i.Properties(p => p.Text("Message").Text("StackTrace")))
                                        )
                                    )
                            )
                        )
                );
        }
    }
}
