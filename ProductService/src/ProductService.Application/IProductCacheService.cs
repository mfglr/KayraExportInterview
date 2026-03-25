using ProductService.Application.Queries;

namespace ProductService.Application
{
    public interface IProductCacheService
    {
        Task<ProductQueryResponse?> GetAsync(Guid id);
        Task UpsertAsync(ProductQueryResponse product);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(IEnumerable<Guid> ids);

        Task<List<ProductQueryResponse>?> GetAsync(int pageSize, DateTime? cursor);
        Task UpsertAsync(int pageSize, DateTime? cursor, List<ProductQueryResponse> products);
    }
}
