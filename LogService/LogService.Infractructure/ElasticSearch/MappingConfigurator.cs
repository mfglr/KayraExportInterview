using Elastic.Clients.Elasticsearch;
using LogService.Domain;

namespace LogService.Infractructure.ElasticSearch
{
    public static class MappingConfigurator
    {
        public static async Task Configure(ElasticsearchClient client) {
            await client.Indices
                .CreateAsync<Log>(
                    index => index
                        .Index(IndexNameProvider.IndexName)
                        .Mappings(mappings => mappings
                            .Properties(
                                properties => properties
                                    .Keyword(x => x.Id)
                                    .Keyword(x => x.ServiceName)
                                    .Date(x => x.Timestamp,x => x.Index(false))
                                    .Keyword(x => x.Level)
                                    .Text(x => x.MessageTemplate)
                                    .Keyword(x => x.TraceId)
                                    .Keyword(x => x.Controller)
                                    .Keyword(x => x.Action)
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
