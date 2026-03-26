namespace LogService.Domain
{
    public class Log(
        string serviceName,
        DateTime timestamp,
        string level,
        string messageTemplate,
        string? traceId,
        IEnumerable<string> requestPaths,
        Exception? exception
    ){
        public string Id { get; private set; } = null!;
        public string ServiceName { get; private set; } = serviceName;
        public DateTime Timestamp { get; private set; } = timestamp;
        public string Level { get; private set; } = level;
        public string MessageTemplate { get; private set; } = messageTemplate;
        public string? TraceId { get; private set; } = traceId;
        public IEnumerable<string> RequestPaths { get; private set; } = requestPaths;
        public Exception? Exception { get; private set; } = exception;

        public void Create()
        {
            Id = Guid.CreateVersion7().ToString();
        }
    }
}
