using Microsoft.AspNetCore.Identity;

namespace AuthService.Domain
{
    public class User : IdentityUser
    {
        public const int MaxSessionCount = 5;

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

        private void CleanUpExpiredSessions() => _refreshTokens.RemoveAll(x => x.IsExpired());

        public string CreateSession(TimeSpan timeSpan)
        {
            CleanUpExpiredSessions();

            if (_refreshTokens.Count >= MaxSessionCount)
                throw new SessionLimitExceeded();

            var refreshToken = new RefreshToken(timeSpan);
            _refreshTokens.Add(refreshToken);
            return refreshToken.Token;
        }

        public void DeleteSession(string token)
        {
            var refreshToken =
                _refreshTokens.FirstOrDefault(x => x.IsMatching(token)) ??
                throw new SessionNotFound();
            
            _refreshTokens.Remove(refreshToken);
        }
    }
}
