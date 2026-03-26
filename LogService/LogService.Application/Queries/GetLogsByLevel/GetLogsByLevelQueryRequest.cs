using MediatR;

namespace LogService.Application.Queries.GetLogsByLevel
{
    public record GetLogsByLevelQueryRequest(string Level, int Page, int PageSize) : IRequest<List<LogResponse>>;
}
