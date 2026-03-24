namespace AuthService.Domain
{
    public class UserCreatorDomainService(IUserRepository userRepository)
    {
        public async Task<User> CreateAsync(Email email, TimeSpan refreshTokenValidtyPeriod, CancellationToken cancellationToken = default)
        {
            if (await userRepository.ExistAsync(email))
                throw new EmailAlreadyTaken();

            return new(email, refreshTokenValidtyPeriod);
        }
    }
}
