namespace Domain.Test
{
    public class CurrencyUnitTests
    {

        [Fact]
        public void Currencies_ShouldBeValid()
        {
            Assert.Equal("TRY", Currency.TRY.Value);
            Assert.Equal("USD", Currency.USD.Value);
            Assert.Equal("EUR", Currency.EUR.Value);
        }

        [Theory]
        [InlineData("TRY")]
        [InlineData("USD")]
        [InlineData("EUR")]
        public void Currency_ShouldCreateCurrency_WhenValueIsValid(string value)
        {
            var currency = new Currency(value);
            Assert.Equal(value.ToUpper(), currency.Value);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("abC")]
        [InlineData("ABC")]
        [InlineData("")]
        [InlineData(null)]
        public void Currency_ShouldThrowException_WhenValueIsInvalid(string? value)
        {
            Assert.Throws<InvalidCurrency>(() => new Currency(value!));
        }

        [Theory]
        [InlineData("TRY", "TRY")]
        [InlineData("USD", "USD")]
        [InlineData("EUR", "EUR")]
        public void EqualOperator_ShouldBeEqual_WhenValuesAreSame(string x,string y)
        {
            var c1 = new Currency(x);
            var c2 = new Currency(y);

            Assert.True(c1 == c2);
            Assert.False(c1 != c2);
        }

        [Theory]
        [InlineData("TRY", "USD")]
        [InlineData("TRY", "EUR")]
        [InlineData("USD", "TRY")]
        [InlineData("USD", "EUR")]
        [InlineData("EUR", "TRY")]
        [InlineData("EUR", "USD")]
        public void EqualOperator_ShouldNotBeEqual_WhenValuesAreDifferent(string x, string y)
        {
            var c1 = new Currency(x);
            var c2 = new Currency(y);

            Assert.True(c1 != c2);
            Assert.False(c1 == c2);
        }

        [Fact]
        public void Clone_ShouldTrue()
        {
            var original = new Currency("TRY");
            var clone = original.Clone();

            Assert.True(original == clone);
        }
    }
}
