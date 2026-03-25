using Domain.Exceptions;

namespace Domain
{
    public class ProductDescription
    {
        private static readonly int _minLength = 10;
        private static readonly int _maxLength = 2000;

        public string Value { get; private set; }

        public ProductDescription(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidDescriptionException("Product description cannot be empty.");

            if (value.Length < _minLength || value.Length > _maxLength)
                throw new InvalidDescriptionException($"Product description must be between {_minLength} and {_maxLength} characters.");

            Value = value;
        }
    }
}
