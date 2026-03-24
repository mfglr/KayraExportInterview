using AuthService.Domain;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.EfCore
{
    internal class UserRepository(UserManager<User> userManager) : IUserRepository
    {
        public Task CreateAsync(User user, Password pasword, CancellationToken cancellationToken = default) =>
            userManager.CreateAsync(user, pasword.Value);

        public async Task<bool> ExistAsync(Email email) =>
            await userManager.FindByEmailAsync(email.Value) != null;
    }
}
