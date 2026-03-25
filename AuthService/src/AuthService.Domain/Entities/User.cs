using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Domain.Entities
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        private readonly List<RefreshToken> _refreshTokens = [];
        public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens;

        internal User(Email email, TimeSpan refreshTokenValidtyPeriod) : base()
        {
            Email = email.Value;
            UserName = email.GenerateUserName();
            _refreshTokens.Add(new RefreshToken(refreshTokenValidtyPeriod));
            CreatedAt = DateTime.UtcNow;
        }

        //for ef core
        private User() { }

        public void Login(TimeSpan refreshTokenValidtyPeriod)
        {
            _refreshTokens.Clear();
            _refreshTokens.Add(new RefreshToken(refreshTokenValidtyPeriod));
        }

        public void LoginByRefreshToken(string token, TimeSpan refreshTokenValidtyPeriod)
        {
            if (!_refreshTokens.Any(x => x.IsValid(token)))
                throw new InvalidOrExpiredRefreshTokenException();

            _refreshTokens.Clear();
            _refreshTokens.Add(new RefreshToken(refreshTokenValidtyPeriod));
        }
    }
}
