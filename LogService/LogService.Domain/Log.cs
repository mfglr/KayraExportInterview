namespace LogService.Domain
{
    public class Log
    {
        public string ServiceName { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Level { get; private set; }
        public string MessageTemplate { get; private set; }
        public string? TraceId { get; private set; }
        public IEnumerable<string> RequestPaths { get; private set; }
        public Exception? Exception { get; private set; }

        public Log(
            string serviceName,
            DateTime timestamp,
            string level,
            string messageTemplate,
            string? traceId,
            IEnumerable<string> requestPaths,
            Exception? exception
    )
        {
            ServiceName = serviceName;
            Timestamp = timestamp;
            Level = level;
            MessageTemplate = messageTemplate;
            TraceId = traceId;
            RequestPaths = requestPaths;
            Exception = exception;
        }
    }
}
