using AuthService.Application;
using AuthService.Application.Commands.CreateUser;
using AuthService.Application.Commands.DeleteUser;
using AuthService.Application.Commands.Login;
using AuthService.Application.Commands.LoginByRefreshToken;
using AuthService.Application.Commands.UpdateUserName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public Task<CreateUserCommandResponse> Create(CreateUserCommandRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [HttpPost]
        public Task<TokenResponse> Login(LoginCommandRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [HttpPost]
        public Task<TokenResponse> LoginByRefreshToken(LoginByRefreshTokenCommandRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [Authorize("user")]
        [HttpPut]
        public Task UpdateUserName(UpdateUserNameCommandRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [Authorize("user")]
        [HttpDelete]
        public Task Delete(CancellationToken cancellationToken) =>
            mediator.Send(new DeleteUserCommandRequest(), cancellationToken);
    }
}
