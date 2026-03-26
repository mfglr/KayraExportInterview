using Elastic.Clients.Elasticsearch;
using LogService.Domain;

namespace LogService.Infractructure.ElasticSearch
{
    internal static class Pagination
    {
        public static SearchRequestDescriptor<Log> ToPage(this SearchRequestDescriptor<Log> srd, string? cursor, int pageSize) =>
            cursor != null
                ? srd
                    .Sort(s => s.Field(f => f.Field(x => x.Id).Order(SortOrder.Desc)))
                    .SearchAfter(cursor)
                    .Size(pageSize)
                : srd
                    .Sort(s => s.Field(f => f.Field(x => x.Id).Order(SortOrder.Desc)))
                    .Size(pageSize);
    }
}
