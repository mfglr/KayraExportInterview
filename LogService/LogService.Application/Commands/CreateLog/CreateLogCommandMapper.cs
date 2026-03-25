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
                request.ServiceName,
                request.TimeStamp,
                request.Level,
                request.MessageTemplate,
                request.TraceId,
                request.Controller,
                request.Action,
                request.Exception != null ? Map(request.Exception) : null
            ); 
    }
}
