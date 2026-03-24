namespace AuthService.Domain
{
    public class Password
    {
        private static readonly int _minLength = 6;
        private static readonly int _maxLength = 256;

        public string Value { get; private set; }

        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < _minLength || value.Length > _maxLength)
                throw new InvalidPassword($"Password must be between {_minLength} and {_maxLength} characters.");
            Value = value;
        }

    }
}
