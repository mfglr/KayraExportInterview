using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;

namespace AuthService.Domain.Test
{
    public class PasswordUnitTests
    {
        [Fact]
        public void Constructor_Should_Create_Password_With_Valid_Value()
        {
            var validPassword = "MyStrongPass123";

            var password = new Password(validPassword);

            Assert.Equal(validPassword, password.Value);
        }

        [Theory]
        [InlineData("123456")]
        [InlineData("Abcdef")]
        [InlineData("Password123")]
        [InlineData("VeryLongPassword1234567890ThatIsLongerThanMinButShorterThanMax")]
        [InlineData("P@ssw0rd!")]
        public void Constructor_Should_Accept_Valid_Password_Lengths(string validPassword)
        {
            var password = new Password(validPassword);

            Assert.Equal(validPassword, password.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t\n")]
        public void Constructor_Should_Throw_Exception_For_Null_Or_Whitespace_Password(string? invalidPassword)
        {
            var exception = Assert.Throws<InvalidPasswordException>(() => new Password(invalidPassword!));

            Assert.Contains("between 6 and 256", exception.Message);
        }

        [Fact]
        public void Constructor_Should_Throw_Exception_For_Password_Shorter_Than_MinLength()
        {
            var shortPassword = "12345";

            Assert.Throws<InvalidPasswordException>(() => new Password(shortPassword));

        }

        [Fact]
        public void Constructor_Should_Throw_Exception_For_Password_Longer_Than_MaxLength()
        {
            var longPassword = new string('A', 257);

            Assert.Throws<InvalidPasswordException>(() => new Password(longPassword));
        }

        [Fact]
        public void Constructor_Should_Throw_Exception_For_Exactly_MaxLength_Plus_One()
        {
            var longPassword = new string('X', 257);

            Assert.Throws<InvalidPasswordException>(() => new Password(longPassword));
        }

        [Fact]
        public void Value_Should_Be_Immutable_After_Creation()
        {
            var password = new Password("InitialPass123");

            Assert.Equal("InitialPass123", password.Value);
        }

        [Fact]
        public void MinLength_Should_Be_6()
        {
            var shortPassword = "12345";

            Assert.Throws<InvalidPasswordException>(() => new Password(shortPassword));
        }

        [Fact]
        public void MaxLength_Should_Be_256()
        {
            var validMaxPassword = new string('A', 256);
            var invalidMaxPassword = new string('A', 257);

            var validPass = new Password(validMaxPassword);

            Assert.Equal(validMaxPassword, validPass.Value);
            Assert.Throws<InvalidPasswordException>(() => new Password(invalidMaxPassword));
        }
    }
}
