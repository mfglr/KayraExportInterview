using LogService.Domain;

namespace LogService.Application.Commands.CreateLog
{
    internal class CreateLogCommandMapper
    {
        public Domain.Exception Map(CreateLogCommandRequest_Exception exception) =>
            new(
                exception.Message,
                exception.StackTrace,
                exception.InnerException != null ? Map(exception.InnerException) : null
            );

        public Log Map(CreateLogCommandRequest request) =>
            new(
                Guid.CreateVersion7().ToString(),
                request.ServiceName,
                request.Timestamp,
                request.Level,
                request.MessageTemplate,
                request.TraceId,
                request.RequestPaths,
                request.Exception != null ? Map(request.Exception) : null
            ); 
    }
}
