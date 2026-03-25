using MediatR;

namespace LogService.Application.Queries.GetLogsByTraceId
{
    public record GetLogsByTraceIdQueryRequest(string TraceId, string? Cursor, int PageSize) : IRequest<List<LogResponse>>;
}
