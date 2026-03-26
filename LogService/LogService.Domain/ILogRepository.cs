namespace LogService.Domain
{
    public interface ILogRepository
    {
        Task CreateAsync(Log log, CancellationToken cancellationToken = default);
        Task CreateAsync(IEnumerable<Log> logs, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<Log>> GetByTraceIdAsync(string traceId, string? cursor, int pageSize, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Log>> GetByLevelAsync(string level, string? cursor, int pageSize, CancellationToken cancellationToken = default);
    }
}
