using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;

namespace AuthService.Domain.Test
{
    public class UserNameUnitTests
    {
        [Theory]
        [InlineData("mfgglr_5390384396965881201")]
        [InlineData("furkan")]
        [InlineData("muhammed_furkan")]
        [InlineData("user123")]
        [InlineData("test.user")]
        [InlineData("admin-2026")]
        [InlineData("a")]
        [InlineData("user_name.with-dot")]
        [InlineData("1234567890")]
        [InlineData("abcdefghijklmnopqrstuvwxyz")]
        public void Constructor_ShouldSucceed_WithValidUserNames(string validValue)
        {
            var userName = new UserName(validValue);

            Assert.Equal(validValue, userName.Value);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenValueIsNull()
        {
            Assert.Throws<InvalidUserNameException>(() => new UserName(null!));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenValueIsEmpty()
        {
            Assert.Throws<InvalidUserNameException>(() => new UserName(""));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenValueIsWhitespace()
        {
            Assert.Throws<InvalidUserNameException>(() => new UserName("   "));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenValueIsLongerThanMaxLength()
        {
            var tooLong = new string('a', 129);
            
            Assert.Throws<InvalidUserNameException>(() => new UserName(tooLong));
        }

        [Fact]
        public void Constructor_ShouldAccept_ExactlyMaxLength()
        {
            var exactlyMax = new string('a', 128);
            var userName = new UserName(exactlyMax);

            Assert.Equal(exactlyMax, userName.Value);
        }


        [Theory]
        [InlineData("Furkan")]
        [InlineData("user name")]
        [InlineData("user@name")]
        [InlineData("user#name")]
        [InlineData("user$name")]
        [InlineData("user%name")]
        [InlineData("user^name")]
        [InlineData("user&name")]
        [InlineData("user(name)")]
        [InlineData("user+name")]
        [InlineData("user=name")]
        [InlineData("user{name}")]
        [InlineData("user[name]")]
        [InlineData("user|name")]
        [InlineData(@"user\name")]
        [InlineData("user,name")]
        [InlineData("user?name")]
        [InlineData("user!name")]
        [InlineData("user~name")]
        [InlineData("userİ")]
        [InlineData("ğüşıöç")]
        public void Constructor_ShouldThrowException_WhenInvalidCharacterUsed(string invalidValue)
        {
            Assert.Throws<InvalidUserNameException>(() => new UserName(invalidValue));
        }

        [Theory]
        [InlineData("_username")]
        [InlineData("username_")]
        [InlineData(".username")]
        [InlineData("username.")]
        [InlineData("-username")]
        [InlineData("username-")]
        public void Constructor_ShouldAllowSpecialCharacters_AtBeginningAndEnd(string value)
        {
            var userName = new UserName(value);
            Assert.Equal(value, userName.Value);
        }
    }
}
