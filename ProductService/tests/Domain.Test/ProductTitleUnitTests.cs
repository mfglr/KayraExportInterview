using Domain.Exceptions;

namespace Domain.Test
{
    public class ProductTitleUnitTests
    {
        [Theory]
        [InlineData("aaa")]
        [InlineData("aaaa")]
        public void ProductTitle_ShouldSetValue_WhenValid(string value)
        {
            var title = new ProductTitle(value);
            Assert.Equal(value, title.Value);
        }
        [Fact]
        public void ProductTitle_ShouldSetValue_WhenValueIsAtMaxLength()
        {
            var value = new string('a', 256);

            var title = new ProductTitle(value);
            Assert.Equal(value, title.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ProductTitle_ShouldThrowException_WhenValueIsEmptyOrNull(string? value)
        {
            Assert.Throws<InvalidTitleException>(() => new ProductTitle(value));
        }
        [Theory]
        [InlineData("a")]
        [InlineData("aa")]
        public void ProductTitle_ShouldThrowException_WhenInvalid(string value)
        {
            Assert.Throws<InvalidTitleException>(() => new ProductTitle(value));
        }
        [Fact]
        public void ProductTitle_ShouldThrowException_WhenValueIsGreaterThanMaxLength()
        {
            var value = new string('a', 257);
            Assert.Throws<InvalidTitleException>(() => new ProductTitle(value));
        }
    }
}
