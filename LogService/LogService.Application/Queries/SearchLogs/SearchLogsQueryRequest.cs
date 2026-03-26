using MediatR;

namespace LogService.Application.Queries.SearchLogs
{
    public record SearchLogsQueryRequest(
        string? TraceId,
        string? ServiceName,
        string? Level,
        string? Controller,
        string? Action,
        string? Key,
        int Page,
        int PageSize
    ) : IRequest<List<LogResponse>>;
}
