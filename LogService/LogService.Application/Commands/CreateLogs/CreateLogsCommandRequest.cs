using MediatR;

namespace LogService.Application.Commands.CreateLogs
{
    public record CreateLogsCommandRequest_Exception(
        string Message,
        string StackTrace,
        CreateLogsCommandRequest_Exception? InnerException
    );

    public record CreateLogsCommandRequest_Log(
        string ServiceName,
        DateTime TimeStamp,
        string Level,
        string MessageTemplate,
        string? TraceId,
        IEnumerable<string> RequestPaths,
        CreateLogsCommandRequest_Exception? Exception
    );

    public record CreateLogsCommandRequest(IEnumerable<CreateLogsCommandRequest_Log> Logs) : IRequest;
}
