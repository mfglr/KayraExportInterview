using Application.Queries;

namespace Application
{
    public interface IProductSerchCacheService
    {
        public string Id(string key, Guid? cursor, int pageSize) =>
            cursor == null ? $"{key}:{pageSize}" : $"{key}:{cursor}:{pageSize}";

        Task<List<ProductQueryResponse>?> GetAsync(string id);
        Task CreateAsync(string id, List<ProductQueryResponse> products);
    }
}
