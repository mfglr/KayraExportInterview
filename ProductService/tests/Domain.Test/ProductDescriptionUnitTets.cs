using ProductService.Domain;
using ProductService.Domain.Exceptions;
using ProductService.Domain.ValueObjects;

namespace Domain.Test
{
    public class ProductDescriptionUnitTets
    {
        [Theory]
        [InlineData("aaaaaaaaaa")]
        [InlineData("aaaaaaaaaaa")]
        public void ProductDescription_ShouldSetValue_WhenValid(string value)
        {
            var description = new ProductDescription(value);
            Assert.Equal(value, description.Value);
        }
        [Fact]
        public void ProductDescription_ShouldSetValue_WhenValueIsAtMaxLength()
        {
            var value = new string('a', 2000);

            var description = new ProductDescription(value);
            Assert.Equal(value, description.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ProductDescription_ShouldThrowException_WhenValueIsEmptyOrNull(string? value)
        {
            Assert.Throws<InvalidDescriptionException>(() => new ProductDescription(value!));
        }
        [Theory]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        [InlineData("aaaa")]
        [InlineData("aaaaa")]
        [InlineData("aaaaaa")]
        [InlineData("aaaaaaa")]
        [InlineData("aaaaaaaa")]
        [InlineData("aaaaaaaaa")]
        public void ProductDescription_ShouldThrowException_WhenInvalid(string value)
        {
            Assert.Throws<InvalidDescriptionException>(() => new ProductDescription(value));
        }
        [Fact]
        public void ProductDescription_ShouldThrowException_WhenValueIsGreaterThanMaxLength()
        {
            var value = new string('a', 2001);
            Assert.Throws<InvalidDescriptionException>(() => new ProductDescription(value));
        }
    }
}
