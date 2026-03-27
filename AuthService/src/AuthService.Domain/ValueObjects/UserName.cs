using AuthService.Domain.Exceptions;

namespace AuthService.Domain.ValueObjects
{
    public class UserName
    {
        private readonly static int _minLength = 1;
        private readonly static int _maxLength = 128;
        
        private static readonly string _validCharacters = "0123456789abcdefghijklmnopqrstuvwxyz_.-";
        public string Value { get; private set; }

        public UserName(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < _minLength || value.Length > _maxLength)
                throw new InvalidUserNameException($"The username must be between {_minLength} and {_maxLength} characters.");

            foreach (char c in value)
                if (!_validCharacters.Contains(c))
                    throw new InvalidUserNameException($"The username can only contain the following characters: {_validCharacters}");

            Value = value;
        }
    }
}
