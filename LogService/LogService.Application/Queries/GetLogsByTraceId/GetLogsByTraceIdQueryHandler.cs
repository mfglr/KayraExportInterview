using LogService.Domain;
using MediatR;

namespace LogService.Application.Queries.GetLogsByTraceId
{
    internal class GetLogsByTraceIdQueryHandler(
        ILogRepository logRepository,
        LogResponseMapper mapper
    ) : IRequestHandler<GetLogsByTraceIdQueryRequest, List<LogResponse>>
    {
        public async Task<List<LogResponse>> Handle(GetLogsByTraceIdQueryRequest request, CancellationToken cancellationToken)
        {
            var logs = await logRepository.GetByTraceIdAsync(request.TraceId, request.Page, request.PageSize, cancellationToken);
            return [.. logs.Select(mapper.Map)];
        }
    }
}
