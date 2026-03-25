using AuthService.Application.Exceptions;
using AuthService.Domain.Repositories;
using MassTransit;
using MediatR;

namespace AuthService.Application.Commands.DeleteUser
{
    internal class DeleteUserCommandHandler(
        IUserRepository userRepository,
        IPublishEndpoint publishEndpoint,
        DeleteUserCommandMapper mapper,
        IAuthService authService
    ) : IRequestHandler<DeleteUserCommandRequest>
    {
        public async Task Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = 
                await userRepository.GetByIdAsync(authService.UserId, cancellationToken) ??
                throw new UserNotFoundException();

            await userRepository.DeleteAsync(user, cancellationToken);

            var @event = mapper.Map(user);
            await publishEndpoint.Publish(@event,cancellationToken);
        }
    }
}
