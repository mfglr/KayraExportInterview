using Application.Queries;

namespace Application
{
    public interface IProductCacheService
    {
        Task<ProductQueryResponse?> GetAsync(Guid id);
        Task CreateAsync(ProductQueryResponse product);
        Task DeleteAsync(ProductQueryResponse product);
    }
}
