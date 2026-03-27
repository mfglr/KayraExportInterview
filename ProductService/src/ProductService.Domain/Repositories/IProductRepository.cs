using ProductService.Domain.Entities;

namespace ProductService.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> SearchAsync(string? key, DateTime? cursor, int pageSize, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllAsync(DateTime? cursor, int pageSize, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Product>> GetByUserIdAsync(Guid userId,CancellationToken cancellationToken = default);
        Task CreateAsync(Product product, CancellationToken cancellationToken = default);
        void Delete(Product product);
        void Delete(IEnumerable<Product> products);
    }
}
