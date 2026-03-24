using Microsoft.AspNetCore.Identity;

namespace AuthService.Domain
{
    public class User : IdentityUser
    {

        public const int MaxSessionCount = 5;

        private readonly List<RefreshToken> _refreshTokens = [];
        public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens;

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
