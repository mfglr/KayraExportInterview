using AuthService.Application;
using AuthService.Application.Commands.CreateUser;
using AuthService.Application.Commands.Login;
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
    }
}
