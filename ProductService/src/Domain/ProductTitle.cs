namespace Domain
{
    public class ProductTitle
    {
        private readonly static int _minLength = 3;
        private readonly static int _maxLength = 256;
        
        public string Value { get; private set; }

        public ProductTitle(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidProductTitle("Product title cannot be empty.");
            
            if (value.Length < _minLength || value.Length > _maxLength)
                throw new InvalidProductTitle($"Product title must be between {_minLength} and {_maxLength} characters.");

            Value = value;
        }
    }
}
