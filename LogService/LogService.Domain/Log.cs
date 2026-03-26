namespace LogService.Domain
{
    public class Log
    {
        public string Id { get; private set; } = null!;
        public string ServiceName { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Level { get; private set; }
        public string MessageTemplate { get; private set; }
        public string? TraceId { get; private set; }
        public string? Controller { get; private set; }
        public string? Action { get; private set; }
        public Exception? Exception { get; private set; }

        public Log(string serviceName, DateTime timestamp, string level, string messageTemplate, string? traceId, string? controller, string? action, Exception? exception)
        {
            ServiceName = serviceName;
            Timestamp = timestamp;
            Level = level;
            MessageTemplate = messageTemplate;
            TraceId = traceId;
            Controller = controller;
            Action = action;
            Exception = exception;
        }

        public void Create()
        {
            Id = Guid.CreateVersion7().ToString();
        }
    }
}
