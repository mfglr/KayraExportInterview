using MediatR;

namespace AuthService.Application.Commands.CreateUser
{
    public record CreateUserCommandRequest(string Email, string Password) : IRequest<CreateUserCommandResponse>;
}
