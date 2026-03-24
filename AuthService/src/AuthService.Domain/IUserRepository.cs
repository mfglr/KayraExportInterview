namespace AuthService.Domain
{
    public interface IUserRepository
    {
        Task<User?> GetEmailOrUserNameAsync(string key, CancellationToken cancellationToken = default);

        Task CreateAsync(User user, Password pasword, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(Email email, CancellationToken cancellationToken = default);
    }
}
