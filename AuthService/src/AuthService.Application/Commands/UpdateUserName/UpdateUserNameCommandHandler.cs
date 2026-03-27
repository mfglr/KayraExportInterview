using AuthService.Application.Exceptions;
using AuthService.Domain.DomainServices;
using AuthService.Domain.Repositories;
using AuthService.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace AuthService.Application.Commands.UpdateUserName
{
    internal class UpdateUserNameCommandHandler(
        UserNameUpdaterDomainService userNameUpdater,
        IUserRepository userRepository,
        IPublishEndpoint publishEndpoint,
        UpdateUserNameCommandMapper mapper,
        IAuthService authService
    ) : IRequestHandler<UpdateUserNameCommandRequest>
    {
        public async Task Handle(UpdateUserNameCommandRequest request, CancellationToken cancellationToken)
        {
            var userName = new UserName(request.UserName);
            var user = 
                await userRepository.GetByIdAsync(authService.UserId, cancellationToken) ??
                throw new UserNotFoundException();
            
            await userNameUpdater.UpdateAsync(user, userName, cancellationToken);

            var @event = mapper.Map(user);
            await publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}
