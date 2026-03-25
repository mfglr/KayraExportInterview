using AuthService.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace AuthService.Domain.ValueObjects
{
    public class Email
    {
        private readonly static string _emailRegexPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        private readonly static Regex _regex = new(_emailRegexPattern);

        public string Value { get; private set; }

        public Email(string value)
        {
            if (value == null || !_regex.IsMatch(value))
                throw new InvalidEmailException();
            Value = value;
        }

        public string GenerateUserName()
        {
            int i = 0;
            while (Value[i] != '@') i++;
            return $"{Value[..i].ToLower()}_{BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0)}";
        }
    }
}
