using MediatR;

namespace LogService.Application.Queries.GetLogsByLevel
{
    public record GetLogsByLevelQueryRequest(string Level, string? Cursor, int PageSize) : IRequest<List<LogResponse>>;
}
