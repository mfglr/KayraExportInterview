namespace AuthService.Domain.Test
{
    public class UserUnitTests
    {
        [Fact]
        public void CreateSession_ShouldAddNewSession_WhenUnderLimit()
        {
            var user = new User();

            var token = user.CreateSession(TimeSpan.FromMinutes(5));

            Assert.Single(user.RefreshTokens);
            Assert.Equal(token, user.RefreshTokens[0].Token);
        }

        [Fact]
        public void CreateSession_ShouldThrow_WhenSessionLimitExceeded()
        {
            var user = new User();
            for (int i = 0; i < User.MaxSessionCount; i++)
                user.CreateSession(TimeSpan.FromMinutes(5));

            Assert.Throws<SessionLimitExceeded>(() => user.CreateSession(TimeSpan.FromMinutes(5)));
        }

        [Fact]
        public void DeleteSession_ShouldRemoveSession_WhenTokenExists()
        {
            var user = new User();
            var token = user.CreateSession(TimeSpan.FromMinutes(5));

            user.DeleteSession(token);

            Assert.Empty(user.RefreshTokens);
        }

        [Fact]
        public void DeleteSession_ShouldThrow_WhenTokenDoesNotExist()
        {
            var user = new User();
            var token = user.CreateSession(TimeSpan.FromMinutes(5));
            var fakeToken = Guid.NewGuid().ToString();

            Assert.Throws<SessionNotFound>(() => user.DeleteSession(fakeToken));
            Assert.Single(user.RefreshTokens);
        }

        [Fact]
        public void DeleteSession_ExpiredSessionsShouldBeRemoved_WhenCreatingNewSession()
        {
            var user = new User();
            user.CreateSession(TimeSpan.Zero);

            var token = user.CreateSession(TimeSpan.FromMinutes(5));

            Assert.Single(user.RefreshTokens);
            Assert.Equal(token, user.RefreshTokens[0].Token);
        }
    }
}
