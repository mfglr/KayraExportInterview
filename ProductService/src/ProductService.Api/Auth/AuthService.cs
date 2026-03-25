using ProductService.Application;
using System.Security.Claims;

namespace ProductService.Api.Auth
{
    public class AuthService(IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        public Guid UserId =>
            Guid.Parse(
                httpContextAccessor
                    .HttpContext?
                    .User
                    .Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
                    .Value ??
                throw new AuthenticationException()
            );
    }
}
