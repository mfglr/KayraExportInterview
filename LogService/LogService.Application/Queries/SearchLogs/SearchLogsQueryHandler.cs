using LogService.Domain;
using MediatR;

namespace LogService.Application.Queries.SearchLogs
{
    internal class SearchLogsQueryHandler(
        ILogRepository logRepository,
        LogResponseMapper mapper
    ) : IRequestHandler<SearchLogsQueryRequest, List<LogResponse>>
    {
        public async Task<List<LogResponse>> Handle(SearchLogsQueryRequest request, CancellationToken cancellationToken)
        {
            var logs = await logRepository.SearchAsync(
                request.TraceId,
                request.ServiceName,
                request.Level,
                request.Key,
                request.Page,
                request.PageSize,
                cancellationToken
            );
            return [.. logs.Select(mapper.Map)];
        }
    }
}
