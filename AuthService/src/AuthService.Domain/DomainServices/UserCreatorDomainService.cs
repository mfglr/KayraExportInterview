using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Repositories;
using AuthService.Domain.ValueObjects;

namespace AuthService.Domain.DomainServices
{
    public class UserCreatorDomainService(IUserRepository userRepository)
    {
        public async Task<User> CreateAsync(Email email, TimeSpan refreshTokenValidtyPeriod, CancellationToken cancellationToken = default)
        {
            if (await userRepository.ExistAsync(email, cancellationToken))
                throw new EmailAlreadyTakenException();

            return new(email, refreshTokenValidtyPeriod);
        }
    }
}
