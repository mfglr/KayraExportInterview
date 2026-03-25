using MediatR;

namespace LogService.Application.Commands.CreateLog
{
    public record CreateLogCommandRequest_Exception(
        string Message,
        string StackTrace,
        CreateLogCommandRequest_Exception? InnerException
    );

    public record CreateLogCommandRequest(
        string ServiceName,
        DateTime TimeStamp,
        string Level,
        string MessageTemplate,
        string? TraceId,
        string? Controller,
        string? Action,
        CreateLogCommandRequest_Exception? Exception
    ) : IRequest;
}
