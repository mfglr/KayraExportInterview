using LogService.Domain;
using MediatR;

namespace LogService.Application.Commands.CreateLog
{
    internal class CreateLogCommandHandler(ILogRepository logRepository, CreateLogCommandMapper mapper) : IRequestHandler<CreateLogCommandRequest>
    {
        public Task Handle(CreateLogCommandRequest request, CancellationToken cancellationToken)
        {
            var log = mapper.Map(request);
            log.Create();
            return logRepository.CreateAsync(log, cancellationToken);
        }
    }
}
