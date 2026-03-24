using AuthService.Domain;
using MediatR;

namespace AuthService.Application.Commands.LoginByRefreshToken
{
    internal class LoginByRefreshTokenCommandHandler(
        IUserRepository userRepository,
        IAccessTokenGenerator accessTokenGenerator,
        ITokenOptions tokenOptions
    ) : IRequestHandler<LoginByRefreshTokenCommandRequest, TokenResponse>
    {
        public async Task<TokenResponse> Handle(LoginByRefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var user =
                await userRepository.GetByIdAsync(request.Id, cancellationToken) ??
                throw new UserNotFound();

            var refreshTokenValidtyPeriod = TimeSpan.FromDays(tokenOptions.RefreshTokenValidtyPeriod);
            user.LoginByRefreshToken(request.Token, refreshTokenValidtyPeriod);

            return new(
                await accessTokenGenerator.GenerateAsync(user),
                user.RefreshTokens[0].Token
            );
        }
    }
}
