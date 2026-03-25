using ProductService.Application;

namespace ProductService.Infrastructure.EfCore
{
    internal class UnitOfWork(ProductContext context) : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellationToken = default) =>
            context.SaveChangesAsync(cancellationToken);
    }
}
