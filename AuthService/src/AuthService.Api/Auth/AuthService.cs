using AuthService.Application;
using System.Security.Claims;

namespace AuthService.Api.Auth
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
