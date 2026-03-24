using AuthService.Application.Commands.CreateUser;
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
    }
}
