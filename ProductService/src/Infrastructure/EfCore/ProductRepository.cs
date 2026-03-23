using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfCore
{
    internal class ProductRepository(ProductContext context) : IProductRepository
    {
        public Task CreateAsync(Product product, CancellationToken cancellationToken = default) =>
            context.Products.AddAsync(product, cancellationToken).AsTask();

        public void Delete(Product product) =>
            context.Products.Remove(product);

        public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public Task<List<Product>> SearchAsync(string key, Guid? cursor, int pageSize, CancellationToken cancellationToken = default) =>
            context.Products
                .AsNoTracking()
                .Where(
                    x =>
                        (
                            x.Title.Value.ToLower().Contains(key.ToLower()) || 
                            x.Description.Value.ToLower().Contains(key.ToLower())
                        ) &&
                        ( cursor == null || x.Id < cursor )
                )
                .OrderByDescending(x => x.CreatedAt)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
    }
}
