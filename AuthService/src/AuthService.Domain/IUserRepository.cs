namespace AuthService.Domain
{
    public interface IUserRepository
    {
        Task CreateAsync(User user, Password pasword, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(Email email);
    }
}
