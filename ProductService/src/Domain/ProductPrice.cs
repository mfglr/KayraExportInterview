using Domain.Exceptions;

namespace Domain
{
    public class ProductPrice
    {
        public decimal Value { get; private set; }
        public Currency Currency { get; private set; } = null!;

        //for ef core
        private ProductPrice() { }

        public ProductPrice(decimal value, Currency currency)
        {
            ArgumentNullException.ThrowIfNull(currency,nameof(currency));

            if (value <= 0)
                throw new InvalidPriceException();

            Value = value;
            Currency = currency;
        }

        public static bool operator==(ProductPrice left, ProductPrice right)
        {
            if (left.Currency != right.Currency)
                throw new CurrencyMismatch();
            return left.Value == right.Value;
        }
             
        public static bool operator !=(ProductPrice left, ProductPrice right)
        {
            if (left.Currency != right.Currency)
                throw new CurrencyMismatch();
            return left.Value != right.Value;
        }

        public static ProductPrice operator+(int left, ProductPrice right) => new(left + right.Value, right.Currency.Clone());
        public static ProductPrice operator+(ProductPrice left, int right) => new(left.Value + right, left.Currency.Clone());
        public static ProductPrice operator-(int left, ProductPrice right) => new(left - right.Value, right.Currency.Clone());
        public static ProductPrice operator-(ProductPrice left, int right) => new(left.Value - right, left.Currency.Clone());
    }
}
