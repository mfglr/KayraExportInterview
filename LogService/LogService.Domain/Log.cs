using System.Text.Json.Serialization;

namespace LogService.Domain
{
    public class Log(string serviceName, DateTime timeStamp, string level, string messageTemplate, string? traceId, string? controller, string? action, Exception? exception)
    {
        public string Id = Guid.CreateVersion7().ToString();
        public string ServiceName { get; private set; } = serviceName;
        public DateTime TimeStamp { get; private set; } = timeStamp;
        public string Level { get; private set; } = level;
        public string MessageTemplate { get; private set; } = messageTemplate;
        public string? TraceId { get; private set; } = traceId;
        public string? Controller { get; private set; } = controller;
        public string? Action { get; private set; } = action;
        public Exception? Exception { get; private set; } = exception;
    }
}
