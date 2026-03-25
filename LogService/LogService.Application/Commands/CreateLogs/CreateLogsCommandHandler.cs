using LogService.Domain;
using MediatR;

namespace LogService.Application.Commands.CreateLogs
{
    internal class CreateLogsCommandHandler(
        ILogRepository logRepository,
        CreateLogsCommandMapper mapper
    ) : IRequestHandler<CreateLogsCommandRequest>
    {
        public Task Handle(CreateLogsCommandRequest request, CancellationToken cancellationToken) =>
             logRepository.CreateAsync(request.Logs.Select(mapper.Map), cancellationToken);
    }
}
