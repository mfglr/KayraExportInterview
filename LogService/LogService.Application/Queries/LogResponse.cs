namespace LogService.Application.Queries
{
    public record LogResponse_Exception(
        string Message,
        string StackTrace,
        LogResponse_Exception? InnerException
    );

    public record LogResponse(
        string Id,
        string ServiceName,
        DateTime TimeStamp,
        string Level,
        string MessageTemplate,
        string? TraceId,
        IEnumerable<string> RequestPaths,
        LogResponse_Exception? Exception
    );
}
