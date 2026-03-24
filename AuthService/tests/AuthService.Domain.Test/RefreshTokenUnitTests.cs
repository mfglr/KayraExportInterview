namespace AuthService.Domain.Test
{
    public class RefreshTokenUnitTests
    {
        [Fact]
        public void IsValid_ShouldReturnTrue_WhenTokenIsCorrectAndNotExpired()
        {
            var refreshToken = new RefreshToken(TimeSpan.FromMinutes(5));
            var token = refreshToken.Token;

            var result = refreshToken.IsValid(token);

            Assert.True(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenTokenIsIncorrect()
        {
            var refreshToken = new RefreshToken(TimeSpan.FromMinutes(5));
            var wrongToken = Guid.NewGuid().ToString();

            var result = refreshToken.IsValid(wrongToken);

            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenTokenIsExpired()
        {
            var refreshToken = new RefreshToken(TimeSpan.Zero);
            var token = refreshToken.Token;

            var result = refreshToken.IsValid(token);

            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldThrowException_WhenTokenIsNull()
        {
            var refreshToken = new RefreshToken(TimeSpan.FromMinutes(5));

            Assert.Throws<ArgumentNullException>(() => refreshToken.IsValid(null!));
        }
    }
}
