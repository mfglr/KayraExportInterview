using Domain.Exceptions;

namespace Domain.Test
{
    public class ProductPriceUnitTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(1453)]
        public void ProductPrice_ShouldCreateProductPrice_WhenValueAndCurrencyAreValid(decimal value)
        {
            var currency = new Currency("USD");

            var price = new ProductPrice(value, currency);

            Assert.Equal(value, price.Value);
            Assert.Equal(currency, price.Currency);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void ProductPrice_ShouldThrowInvalidPrice_WhenValueIsZeroOrNegative(decimal value)
        {
            var currency = new Currency("USD");
            Assert.Throws<InvalidPriceException>(() => new ProductPrice(value, currency));
        }
        [Fact]
        public void ProductPrice_ShouldThrowException_WhenCurrencyIsNull()
        {
            decimal value = 50m;
            Assert.Throws<ArgumentNullException>(() => new ProductPrice(value, null!));
        }

        
        [Theory]
        [InlineData("TRY", "TRY", 5, 5)]
        [InlineData("USD", "USD", 5, 5)]
        [InlineData("EUR", "EUR", 5, 5)]
        public void EqualOperator_ShouldBeTrue_WhenSameValueAndCurrency(string cs1,string cs2, decimal pd1, decimal pd2)
        {
            var c1 = new Currency(cs1);
            var c2 = new Currency(cs2);

            var p1 = new ProductPrice(pd1, c1);
            var p2 = new ProductPrice(pd2, c2);

            Assert.True(p1 == p2);
            Assert.False(p1 != p2);
        }
        [Theory]
        [InlineData("TRY", "USD")]
        [InlineData("TRY", "EUR")]
        [InlineData("USD", "TRY")]
        [InlineData("USD", "EUR")]
        [InlineData("EUR", "TRY")]
        [InlineData("EUR", "USD")]
        public void EqualOperator_ShouldThrowException_WhenCurrenciesDiffer(string x, string y)
        {
            var p1 = new ProductPrice(100, new Currency(x));
            var p2 = new ProductPrice(100, new Currency(y));

            Assert.Throws<CurrencyMismatch>(() => p1 == p2);
            Assert.Throws<CurrencyMismatch>(() => p1 != p2);
        }
        [Fact]
        public void EqualOperator_ShouldTrue_WhenValuesAreDiffer()
        {
            var p1 = new ProductPrice(10, new Currency("USD"));
            var p2 = new ProductPrice(11, new Currency("USD"));

            Assert.True(p1 != p2);
            Assert.False(p1 == p2);
        }

        [Fact]
        public void ArithmeticAddition_ShouldWorkCorrectly()
        {
            var price = new ProductPrice(100, new Currency("USD"));
            var result1 = price + 50;
            var result2 = 50 + price;

            Assert.Equal(150, result1.Value);
            Assert.NotEqual(price.Currency, result1.Currency);
            Assert.True(price.Currency == result1.Currency);

            Assert.Equal(150, result2.Value);
            Assert.NotEqual(price.Currency, result2.Currency);
            Assert.True(price.Currency == result2.Currency);
        }

        [Fact]
        public void ArithmeticSubtraction_ShouldWorkCorrectly()
        {
            var price = new ProductPrice(100, new Currency("USD"));
            var result1 = price - 40;
            var result2 = 160 - price;

            Assert.Equal(60, result1.Value);
            Assert.NotEqual(price.Currency, result1.Currency);
            Assert.True(price.Currency == result1.Currency);

            Assert.Equal(60, result2.Value);
            Assert.NotEqual(price.Currency, result2.Currency);
            Assert.True(price.Currency == result2.Currency);
        }


    }
}
