using System.Security.Cryptography;
using System.Text;

namespace AuthService.Domain.ValueObjects
{
    public class RefreshToken
    {
        public string Token { get; private set; } //not mapped
        public byte[] TokenHash { get; private set; }
        public DateTime ExpiredAt { get; private set; }

        //for ef core
        private RefreshToken() { }

        public RefreshToken(TimeSpan timeSpan)
        {
            Token = Guid.NewGuid().ToString();
            var bytes = Encoding.UTF8.GetBytes(Token);
            TokenHash = SHA256.HashData(bytes);
            
            ExpiredAt = DateTime.UtcNow.Add(timeSpan);
        }

        public bool IsValid(string token)
        {
            ArgumentNullException.ThrowIfNull(token, nameof(token));

            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = SHA256.HashData(bytes);
            return hash.SequenceEqual(TokenHash) && ExpiredAt > DateTime.UtcNow;
        }
    }
}
