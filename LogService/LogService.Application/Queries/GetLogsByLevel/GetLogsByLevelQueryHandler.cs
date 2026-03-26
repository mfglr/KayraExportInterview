using LogService.Domain;
using MediatR;

namespace LogService.Application.Queries.GetLogsByLevel
{
    internal class GetLogsByLevelQueryHandler(
        ILogRepository logRepository,
        LogResponseMapper mapper
    ) : IRequestHandler<GetLogsByLevelQueryRequest, List<LogResponse>>
    {
        public async Task<List<LogResponse>> Handle(GetLogsByLevelQueryRequest request, CancellationToken cancellationToken)
        {
            var logs = await logRepository.GetByLevelAsync(request.Level, request.Cursor, request.PageSize, cancellationToken);
            return [.. logs.Select(mapper.Map)];
        }
    }
}
