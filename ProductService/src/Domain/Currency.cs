namespace Domain
{
    public class Currency
    {
        private static class CurrencyValues
        {
            public static string TRY = "TRY";
            public static string USD = "USD";
            public static string EUR = "EUR";

            public static bool IsValid(string value) => value == TRY || value == USD || value == EUR;
        }

        public string Value { get; }

        public Currency(string value)
        {
            if (!CurrencyValues.IsValid(value))
                throw new InvalidCurrency();

            Value = value;
        }

        public Currency Clone() => new(Value);

        public static readonly Currency TRY = new("TRY");
        public static readonly Currency USD = new("USD");
        public static readonly Currency EUR = new("EUR");

        public static bool operator==(Currency left, Currency right) => left.Value == right.Value;
        public static bool operator!=(Currency left, Currency right) => left.Value != right.Value;
    }
}
