namespace AuthService.Application
{
    public interface IUnitOfWork
    {
        Task CreateTransactionAsync(CancellationToken cancellationToken);
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
