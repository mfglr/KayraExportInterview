using Application.Queries;
using Application.Queries.SearchProduct;

namespace Application
{
    public interface IProductListCacheService
    {
        public string Id(Guid? cursor, int pageSize) =>
            cursor == null ? $"list:{pageSize}" : $"list:{pageSize}:{cursor}";

        Task<List<ProductQueryResponse>?> GetAsync(string id);
        Task CreateAsync(string id, List<ProductQueryResponse> products);
    }
}
