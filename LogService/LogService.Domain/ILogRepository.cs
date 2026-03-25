namespace LogService.Domain
{
    public interface ILogRepository
    {
        Task CreateAsync(Log log, CancellationToken cancellationToken = default);
        Task CreateAsync(IEnumerable<Log> logs, CancellationToken cancellationToken = default);
    }
}
