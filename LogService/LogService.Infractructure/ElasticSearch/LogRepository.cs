using Elastic.Clients.Elasticsearch;
using LogService.Domain;

namespace LogService.Infractructure.ElasticSearch
{
    internal class LogRepository(ElasticsearchClient client) : ILogRepository
    {
        public Task CreateAsync(Log log, CancellationToken cancellationToken = default) =>
            client.IndexAsync(log, x => x.Index(IndexNameProvider.IndexName), cancellationToken: cancellationToken);

        public Task CreateAsync(IEnumerable<Log> logs, CancellationToken cancellationToken = default) =>
            client.IndexManyAsync(logs, IndexNameProvider.IndexName, cancellationToken: cancellationToken);

        public async Task<IReadOnlyCollection<Log>> SearchAsync(
            string? traceId,
            string? serviceName,
            string? level,
            string? key,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default
        )
        {
            var result = await client.SearchAsync<Log>(
                search =>
                    search
                        .Indices(IndexNameProvider.IndexName)
                        .ToQuery(traceId,serviceName,level,key)
                        .From(page * pageSize)
                        .Size(pageSize),
                cancellationToken: cancellationToken
            );
            return result.Documents;
        }
    }
}
