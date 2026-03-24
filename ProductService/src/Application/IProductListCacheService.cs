using Application.Queries;

namespace Application
{
    public interface IProductListCacheService
    {
        public string Id(DateTime? cursor, int pageSize) =>
            cursor == null ? $"list:{pageSize}" : $"list:{pageSize}:{cursor}";

        Task<List<ProductQueryResponse>?> GetAsync(string id);
        Task CreateAsync(string id, List<ProductQueryResponse> products);
    }
}
