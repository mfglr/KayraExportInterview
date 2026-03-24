using MediatR;

namespace AuthService.Application.Commands.LoginByRefreshToken
{
    public record LoginByRefreshTokenCommandRequest(Guid Id, string Token) : IRequest<TokenResponse>;
}
