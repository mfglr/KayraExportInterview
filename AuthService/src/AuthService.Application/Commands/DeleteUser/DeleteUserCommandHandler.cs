using AuthService.Domain;
using MassTransit;
using MediatR;

namespace AuthService.Application.Commands.DeleteUser
{
    internal class DeleteUserCommandHandler(
        IUserRepository userRepository,
        IPublishEndpoint publishEndpoint,
        DeleteUserCommandMapper mapper
    ) : IRequestHandler<DeleteUserCommandRequest>
    {
        public async Task Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = 
                await userRepository.GetByIdAsync(request.Id, cancellationToken) ??
                throw new UserNotFound();

            await userRepository.DeleteAsync(user, cancellationToken);

            var @event = mapper.Map(user);
            await publishEndpoint.Publish(@event,cancellationToken);
        }
    }
}
