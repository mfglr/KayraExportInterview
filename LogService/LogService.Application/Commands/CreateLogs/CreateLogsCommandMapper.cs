using LogService.Domain;

namespace LogService.Application.Commands.CreateLogs
{
    internal class CreateLogsCommandMapper
    {
        public Domain.Exception Map(CreateLogsCommandRequest_Exception exception) =>
            new(
                exception.Message,
                exception.StackTrace,
                exception.InnerException != null ? Map(exception.InnerException) : null
            );

        public Log Map(CreateLogsCommandRequest_Log request) =>
            new(
                request.ServiceName,
                request.TimeStamp,
                request.Level,
                request.MessageTemplate,
                request.TraceId,
                request.RequestPaths,
                request.Exception != null ? Map(request.Exception) : null
            );
    }
}
