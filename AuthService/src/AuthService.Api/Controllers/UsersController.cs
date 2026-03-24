using AuthService.Application;
using AuthService.Application.Commands.CreateUser;
using AuthService.Application.Commands.DeleteUser;
using AuthService.Application.Commands.Login;
using AuthService.Application.Commands.LoginByRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public Task<CreateUserCommandResponse> Create(CreateUserCommandRequest request, CancellationToken cancellationToken) =>
            sender.Send(request, cancellationToken);

        [HttpPost]
        public Task<TokenResponse> Login(LoginCommandRequest request, CancellationToken cancellationToken) =>
            sender.Send(request, cancellationToken);

        [HttpPost]
        public Task<TokenResponse> LoginByRefreshToken(LoginByRefreshTokenCommandRequest request, CancellationToken cancellationToken) =>
            sender.Send(request, cancellationToken);

        [HttpDelete("{id:guid}")]
        public Task Delete(Guid id,CancellationToken cancellationToken) =>
            sender.Send(new DeleteUserCommandRequest(id), cancellationToken);
    }
}
