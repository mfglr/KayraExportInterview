using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;

namespace ProductService.Infrastructure.EfCore
{
    internal class ProductRepository(ProductContext context) : IProductRepository
    {
        public Task CreateAsync(Product product, CancellationToken cancellationToken = default) =>
            context.Products.AddAsync(product, cancellationToken).AsTask();

        public void Delete(Product product) =>
            context.Products.Remove(product);

        public void Delete(IEnumerable<Product> products) =>
            context.Products.RemoveRange(products);

        public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public Task<List<Product>> SearchAsync(string key, DateTime? cursor, int pageSize, CancellationToken cancellationToken = default) =>
            context.Products
                .AsNoTracking()
                .Where(
                    x =>
                        (
                            x.Title.Value.ToLower().Contains(key.ToLower()) || 
                            x.Description.Value.ToLower().Contains(key.ToLower())
                        ) &&
                        ( cursor == null || x.CreatedAt < cursor )
                )
                .OrderByDescending(x => x.CreatedAt)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

        public Task<List<Product>> GetAllAsync(DateTime? cursor, int pageSize, CancellationToken cancellationToken = default) =>
            context.Products
                .AsNoTracking()
                .Where(x => cursor == null || x.CreatedAt < cursor)
                .OrderByDescending(x => x.CreatedAt)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

        public Task<List<Product>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
            context.Products.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
    }
}
