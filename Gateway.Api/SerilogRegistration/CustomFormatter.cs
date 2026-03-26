using Serilog.Events;
using Serilog.Formatting;
using System.Text.Json;

namespace Gateway.Api.SerilogRegistration
{
    public class CustomFormatter(string serviceName) : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            var logObj = new Dictionary<string, object>
            {
                ["ServiceName"] = serviceName,
                ["Timestamp"] = logEvent.Timestamp,
                ["Level"] = logEvent.Level.ToString(),
                ["MessageTemplate"] = logEvent.MessageTemplate.Text,
            };

            var traceId = logEvent.TraceId.ToString();
            if (!string.IsNullOrWhiteSpace(traceId))
                logObj["TraceId"] = traceId;

            if (logEvent.Exception != null)
            {
                logObj["Exception"] = new
                {
                    logEvent.Exception.Message,
                    logEvent.Exception.StackTrace,
                    InnerException = logEvent.Exception.InnerException != null
                        ? new
                        {
                            logEvent.Exception.InnerException.Message,
                            logEvent.Exception.InnerException.StackTrace,
                        }
                        : null,
                };
            }

            var requestPath = logEvent.Properties.Where(x => x.Key == "RequestPath").FirstOrDefault().Value?.ToString();

            if (!string.IsNullOrWhiteSpace(requestPath))
            {
                var requestPaths = requestPath.Replace("\\", "").Replace("\"", "").Replace("/", " ").Trim().Split(" ");
                logObj["RequestPaths"] = requestPaths;
            }

            output.WriteLine(JsonSerializer.Serialize(logObj));
        }
    }
}
