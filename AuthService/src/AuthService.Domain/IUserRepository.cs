namespace AuthService.Domain
{
    public interface IUserRepository
    {
        Task<User?> GetEmailOrUserNameAsync(string key, CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task CreateAsync(User user, Password pasword, CancellationToken cancellationToken = default);
        Task DeleteAsync(User user, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(Email email, CancellationToken cancellationToken = default);
        Task<bool> CheckPasswordAsync(User user, string password, CancellationToken cancellationToken = default);
    }
}
