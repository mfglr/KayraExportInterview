using AuthService.Domain;

namespace AuthService.Application
{
    public interface IAccessTokenGenerator
    {
        Task<string> GenerateAsync(User user);
    }
}
