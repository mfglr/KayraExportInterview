using ProductService.Application;

namespace Infrastructure.EfCore
{
    internal class UnitOfWork(ProductContext context) : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellationToken = default) =>
            context.SaveChangesAsync(cancellationToken);
    }
}
