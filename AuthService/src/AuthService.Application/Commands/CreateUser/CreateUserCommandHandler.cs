using AuthService.Domain;
using MassTransit;
using MediatR;

namespace AuthService.Application.Commands.CreateUser
{
    internal class CreateUserCommandHandler(
        IAccessTokenGenerator accessTokenGenerator,
        IUserRepository userRepository,
        UserCreatorDomainService userCreator,
        IPublishEndpoint publishEndpoint,
        CreateUserCommandMapper mapper,
        ITokenOptions tokenOptions
    ) : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);
            var password = new Password(request.Password);
            var refreshTokenValidtyPeriod = TimeSpan.FromDays(tokenOptions.RefreshTokenValidtyPeriod);
            var user = await userCreator.CreateAsync(email, refreshTokenValidtyPeriod, cancellationToken);

            await userRepository.CreateAsync(user, password, cancellationToken);
            await userRepository.AddRoleAsync(user, "user", cancellationToken);

            var @event = mapper.Map(user);
            await publishEndpoint.Publish(@event, cancellationToken);

            var token = new TokenResponse(
                await accessTokenGenerator.GenerateAsync(user),
                user.RefreshTokens[0].Token
            );
            return new(Guid.Parse(user.Id), token);
        }
    }
}
