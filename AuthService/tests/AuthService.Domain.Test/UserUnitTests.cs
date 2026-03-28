using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;

namespace AuthService.Domain.Test
{
    public class UserTests
    {
        private readonly TimeSpan _defaultRefreshTokenValidity = TimeSpan.FromDays(7);
        private readonly Email _testEmail = new("test@example.com");

        [Fact]
        public void Constructor_ShouldCreateUser_WithValidEmailAndInitialRefreshToken()
        {
            var user = new User(_testEmail, _defaultRefreshTokenValidity);

            Assert.NotNull(user);
            Assert.Equal(_testEmail.Value, user.Email);
            Assert.False(string.IsNullOrEmpty(user.UserName));
            Assert.True(user.CreatedAt <= DateTime.UtcNow);
            Assert.True(user.CreatedAt > DateTime.UtcNow.AddSeconds(-10));
            Assert.Null(user.UpdatedAt);

            Assert.Single(user.RefreshTokens);
            var refreshToken = user.RefreshTokens.First();
            Assert.NotNull(refreshToken);
            Assert.True(refreshToken.IsValid(refreshToken.Token));
        }

        [Fact]
        public void Constructor_ShouldGenerateUserName_FromEmail()
        {
            var user = new User(_testEmail, _defaultRefreshTokenValidity);

            Assert.False(string.IsNullOrEmpty(user.UserName));
            Assert.Contains("test", user.UserName, StringComparison.OrdinalIgnoreCase);
            Assert.Equal(user.UserName.ToUpper(), user.NormalizedUserName);
        }

        [Fact]
        public void Login_ShouldClearPreviousTokens_AndAddNewOne()
        {
            var user = new User(_testEmail, _defaultRefreshTokenValidity);
            var oldToken = user.RefreshTokens.First().Token;

            user.Login(TimeSpan.FromDays(30));

            Assert.Single(user.RefreshTokens);
            Assert.NotEqual(oldToken, user.RefreshTokens.First().Token);
            Assert.True(user.RefreshTokens.First().ExpiredAt > DateTime.UtcNow);
        }

        [Fact]
        public void LoginByRefreshToken_ShouldSucceed_WhenTokenIsValid()
        {
            var user = new User(_testEmail, _defaultRefreshTokenValidity);
            var validToken = user.RefreshTokens.First().Token;

            user.LoginByRefreshToken(validToken, TimeSpan.FromDays(14));

            Assert.Single(user.RefreshTokens);
            Assert.NotEqual(validToken, user.RefreshTokens.First().Token);
        }

        [Fact]
        public void LoginByRefreshToken_ShouldThrowException_WhenTokenIsInvalid()
        {
            var user = new User(_testEmail, _defaultRefreshTokenValidity);

            Assert.Throws<InvalidOrExpiredRefreshTokenException>(() =>
                user.LoginByRefreshToken("invalid-token-12345", TimeSpan.FromDays(7)));
        }

        [Fact]
        public void LoginByRefreshToken_ShouldThrowException_WhenTokenIsExpired()
        {
            var shortValidity = TimeSpan.Zero;
            var user = new User(_testEmail, shortValidity);
            var token = user.RefreshTokens.First().Token;

            Assert.Throws<InvalidOrExpiredRefreshTokenException>(() =>
                user.LoginByRefreshToken(token, TimeSpan.FromDays(7)));
        }

        [Fact]
        public void UpdateUserName_Should_UpdateUserName_And_NormalizedUserName_And_UpdatedAt()
        {
            var user = new User(_testEmail, _defaultRefreshTokenValidity);
            var newUserName = new UserName("newusername123");
            var dateBefore = DateTime.UtcNow;

            user.UpdateUserName(newUserName);

            Assert.Equal("newusername123", user.UserName);
            Assert.Equal("NEWUSERNAME123", user.NormalizedUserName);
            Assert.NotNull(user.UpdatedAt);
            Assert.True(user.UpdatedAt > dateBefore);
            Assert.True(user.UpdatedAt <= DateTime.UtcNow);
            Assert.True(user.UpdatedAt > DateTime.UtcNow.AddSeconds(-5));
        }
    }
}
