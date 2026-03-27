using MediatR;

namespace LogService.Application.Queries.GetLogsByTraceId
{
    public record GetLogsByTraceIdQueryRequest(string TraceId, int Page, int PageSize = 20) : IRequest<List<LogResponse>>;
}
