using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using LogService.Domain;

namespace LogService.Infractructure.ElasticSearch
{
    internal static class QueryExtension
    {
        private static BoolQueryDescriptor<Log> ToBoolQuery(
            this BoolQueryDescriptor<Log> bqd,
            string? traceId,
            string? serviceName,
            string? level,
            string? key
        )
        {
            if (!string.IsNullOrWhiteSpace(traceId))
                bqd = bqd.Filter(f => f.Term(t => t.Field(f => f.TraceId).Value(traceId)));
            if (!string.IsNullOrWhiteSpace(serviceName))
                bqd = bqd.Filter(f => f.Term(t => t.Field(f => f.ServiceName).Value(serviceName)));
            if (!string.IsNullOrWhiteSpace(level))
                bqd = bqd.Filter(f => f.Term(x => x.Field(x => x.Level).Value(level)));
            if (!string.IsNullOrWhiteSpace(key))
                bqd = bqd
                    .Must(
                        f => f.MultiMatch(
                            p => p.Fields(
                                f => f.RequestPaths,
                                f => f.MessageTemplate,
                                f => f.Exception.Message,
                                f => f.Exception.StackTrace,
                                f => f.Exception.InnerException.Message,
                                f => f.Exception.InnerException.StackTrace
                            ).Query(key)
                        )
                    );
            return bqd;
        }

        public static SearchRequestDescriptor<Log> ToQuery(
            this SearchRequestDescriptor<Log> srd,
            string? traceId,
            string? serviceName,
            string? level,
            string? key
        ) => srd
                .Query(x => x.Bool(b => b.ToBoolQuery(traceId, serviceName, level, key)));
    }
}
