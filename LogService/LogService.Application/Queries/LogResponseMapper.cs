using LogService.Domain;

namespace LogService.Application.Queries
{
    internal class LogResponseMapper
    {
        public LogResponse_Exception Map(Domain.Exception exception) =>
            new(
                exception.Message,
                exception.StackTrace,
                exception.InnerException != null ? Map(exception.InnerException) : null
            );

        public LogResponse Map(Log log) =>
            new(
                log.Id,
                log.ServiceName,
                log.TimeStamp,
                log.Level,
                log.MessageTemplate,
                log.TraceId,
                log.Controller,
                log.Action,
                log.Exception != null ? Map(log.Exception) : null
            );
    }
}
