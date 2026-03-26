using LogService.Application.Queries;
using LogService.Application.Queries.GetLogsByLevel;
using LogService.Application.Queries.GetLogsByTraceId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LogService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public Task<List<LogResponse>> GetByLevels([FromQuery]GetLogsByLevelQueryRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [HttpGet]
        public Task<List<LogResponse>> GetByTraceId([FromQuery] GetLogsByTraceIdQueryRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);
    }
}
