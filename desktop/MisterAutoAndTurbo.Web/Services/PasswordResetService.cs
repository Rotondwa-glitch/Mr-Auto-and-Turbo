using System.Collections.Concurrent;

namespace MisterAutoAndTurbo.Web.Services
{
    /// <summary>
    /// In-memory store for password recovery codes. Codes expire after a short time.
    /// </summary>
    public class PasswordResetService
    {
        private readonly ConcurrentDictionary<string, (string Code, DateTime Expires)> _codes = new();
        private static readonly TimeSpan CodeLifetime = TimeSpan.FromMinutes(15);

        public string GenerateCode(string email)
        {
            var code = new Random().Next(100000, 999999).ToString();
            _codes[email.ToLowerInvariant()] = (code, DateTime.UtcNow.Add(CodeLifetime));
            return code;
        }

        public bool ValidateCode(string email, string code)
        {
            var key = email.ToLowerInvariant();
            if (!_codes.TryGetValue(key, out var entry))
                return false;

            if (DateTime.UtcNow > entry.Expires)
            {
                _codes.TryRemove(key, out _);
                return false;
            }

            return entry.Code == code;
        }

        public void InvalidateCode(string email)
        {
            _codes.TryRemove(email.ToLowerInvariant(), out _);
        }
    }
}
