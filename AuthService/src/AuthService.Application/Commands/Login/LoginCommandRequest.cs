using MediatR;

namespace AuthService.Application.Commands.Login
{
    public record LoginCommandRequest(string Key, string Password) : IRequest<TokenResponse>;
}
