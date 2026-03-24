using AuthService.Application;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuthService.Infrastructure.EfCore
{
    internal class UnitOfWork(AuthContext context) : IUnitOfWork
    {
        private IDbContextTransaction? _dbContextTransaction;

        public async Task CreateTransactionAsync(CancellationToken cancellationToken)
        {
            _dbContextTransaction = await context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync(cancellationToken);
            if (_dbContextTransaction != null)
                await _dbContextTransaction.CommitAsync(cancellationToken);
        }
    }
}
