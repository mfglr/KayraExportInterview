using AuthService.Domain;
using MediatR;

namespace AuthService.Application.Commands.Login
{
    internal class LoginCommandHandler(
        IUserRepository userRepository,
        ITokenOptions tokenOptions,
        IAccessTokenGenerator accessTokenGenerator
    ) : IRequestHandler<LoginCommandRequest, TokenResponse>
    {
        public async Task<TokenResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = 
                await userRepository.GetEmailOrUserNameAsync(request.Key, cancellationToken) ??
                throw new UserNotFound();

            if (!await userRepository.CheckPasswordAsync(user, request.Password, cancellationToken))
                throw new InvalidCredentials();

            var refreshTokenValidtyPeriod = TimeSpan.FromDays(tokenOptions.RefreshTokenValidtyPeriod);
            user.Login(refreshTokenValidtyPeriod);

            return new(
                await accessTokenGenerator.GenerateAsync(user),
                user.RefreshTokens[0].Token
            );
        }
    }
}
