using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AuthService.Domain.ValueObjects;

namespace AuthService.Domain.DomainServices
{
    public class UserNameUpdaterDomainService(IUserRepository userRepository)
    {
        public async Task UpdateAsync(User user, UserName userName, CancellationToken cancellationToken)
        {
            if (await userRepository.ExistAsync(userName,cancellationToken))
                throw new UserNameAlreadyTakeException();

            user.UpdateUserName(userName);
        }
    }
}
