using AuthService.Domain;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.EfCore
{
    internal class UserRepository(UserManager<User> userManager) : IUserRepository
    {
        public async Task<User?> GetEmailOrUserNameAsync(string key, CancellationToken cancellationToken = default) =>
            await userManager.FindByEmailAsync(key) ??
            await userManager.FindByNameAsync(key);

        public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
           userManager.FindByIdAsync(id.ToString());

        public Task CreateAsync(User user, Password pasword, CancellationToken cancellationToken = default) =>
            userManager.CreateAsync(user, pasword.Value);

        public async Task<bool> ExistAsync(Email email, CancellationToken cancellation = default) =>
            await userManager.FindByEmailAsync(email.Value) != null;
    }
}
