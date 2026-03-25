namespace ProductService.Application
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
