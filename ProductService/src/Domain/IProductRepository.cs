namespace Domain
{
    public interface IProductRepository
    {
        Task<List<Product>> SearchAsync(string key, Guid? cursor, int pageSize, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllAsync(Guid? cursor, int pageSize, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(Product product, CancellationToken cancellationToken = default);
        void Delete(Product product);
    }
}
