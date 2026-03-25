using AuthService.Domain.Entities;

namespace AuthService.Application
{
    public interface IAccessTokenGenerator
    {
        Task<string> GenerateAsync(User user);
    }
}
