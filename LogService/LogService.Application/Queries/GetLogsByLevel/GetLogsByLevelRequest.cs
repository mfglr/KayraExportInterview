using MediatR;

namespace LogService.Application.Queries.GetLogsByLevel
{
    public record GetLogsByLevelRequest(string Level, string? Cursor, int PageSize) : IRequest<List<LogResponse>>;
}
