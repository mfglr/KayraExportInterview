using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;

namespace AuthService.Domain.Test
{
    public class EmailUnitTests
    {
        [Fact]
        public void Constructor_Should_Create_Email_With_Valid_Address()
        {
            string validEmail = "test@example.com";

            var email = new Email(validEmail);

            Assert.Equal(validEmail, email.Value);
        }

        [Theory]
        [InlineData("user@gmail.com")]
        [InlineData("user_name123@subdomain.example.co.uk")]
        [InlineData("hello.world@my-domain.com")]
        [InlineData("test123@domain.co")]
        public void Constructor_Should_Accept_Valid_Email_Formats(string validEmail)
        {
            var email = new Email(validEmail);

            Assert.Equal(validEmail, email.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("invalid-email")]
        [InlineData("user@")]
        [InlineData("@domain.com")]
        [InlineData("user@domain")]
        [InlineData("user@domain.c")]
        [InlineData("user@domain.toolonggggg")]
        [InlineData("user name@domain.com")]
        [InlineData("user@domain..com")]
        public void Constructor_Should_Throw_InvalidEmailException_For_Invalid_Emails(string invalidEmail)
        {
            Assert.Throws<InvalidEmailException>(() => new Email(invalidEmail));
        }

        [Fact]
        public void GenerateUserName_Should_Return_Username_With_Lowercase_Part_And_Random_Number()
        {
            var email = new Email("Test.User@Example.com");

            string userName = email.GenerateUserName();

            Assert.NotNull(userName);
            Assert.False(string.IsNullOrEmpty(userName));

            Assert.StartsWith("test.user_", userName);

            string[] parts = userName.Split('_');
            Assert.Equal(2, parts.Length);
            Assert.True(long.TryParse(parts[1], out _), "Alt çizgiden sonraki kısım sayı olmalı");
        }

        [Fact]
        public void GenerateUserName_Should_Generate_Different_Values_Each_Time()
        {
            var email = new Email("user@example.com");

            string username1 = email.GenerateUserName();
            string username2 = email.GenerateUserName();

            Assert.NotEqual(username1, username2);
        }

        [Theory]
        [InlineData("john.doe@gmail.com", "john.doe")]
        [InlineData("test123@domain.co.uk", "test123")]
        [InlineData("user_name@sub.example.com", "user_name")]
        public void GenerateUserName_Should_Take_Part_Before_At_Symbol(string emailAddress, string expectedPrefix)
        {
            var email = new Email(emailAddress);

            string userName = email.GenerateUserName();

            Assert.StartsWith(expectedPrefix.ToLower(), userName);
            Assert.Contains("_", userName);
        }

        [Fact]
        public void Value_Should_Be_Immutable_After_Creation()
        {
            var email = new Email("original@example.com");

            Assert.Equal("original@example.com", email.Value);
        }
    }
}
