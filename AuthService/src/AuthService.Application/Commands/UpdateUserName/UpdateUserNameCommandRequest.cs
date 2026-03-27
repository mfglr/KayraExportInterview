using MediatR;

namespace AuthService.Application.Commands.UpdateUserName
{
    public record UpdateUserNameCommandRequest(string UserName) : IRequest;
}
