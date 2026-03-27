using MediatR;

namespace LogService.Application.Queries.SearchLogs
{
    public record SearchLogsQueryRequest(
        string? TraceId,
        string? ServiceName,
        string? Level,
        string? Key,
        int Page,
        int PageSize = 20
    ) : IRequest<List<LogResponse>>;
}
