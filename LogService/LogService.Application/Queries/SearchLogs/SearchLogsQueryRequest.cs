using MediatR;

namespace LogService.Application.Queries.SearchLogs
{
    public record SearchLogsQueryRequest(
        string? TraceId,
        string? ServiceName,
        string? Level,
        string? Key,
        int Page,
        int PageSize
    ) : IRequest<List<LogResponse>>;
}
