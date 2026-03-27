using LogService.Application.Queries;
using LogService.Application.Queries.SearchLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LogService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public Task<List<LogResponse>> Search([FromQuery] SearchLogsQueryRequest reques,CancellationToken cancellationToken) =>
            mediator.Send(reques, cancellationToken);
    }
}
