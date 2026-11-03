using Microsoft.AspNetCore.Identity;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Services
{
    /// <summary>
    /// Hashes passwords with ASP.NET Identity PasswordHasher.
    /// </summary>
    public static class PasswordService
    {
        private static readonly PasswordHasher<UserAccount> Hasher = new();

        private static bool LooksHashed(string? value) =>
            !string.IsNullOrEmpty(value) && value.StartsWith("AQAAAA", StringComparison.Ordinal);

        public static string Hash(string password) => Hasher.HashPassword(new UserAccount(), password);

        public static bool Verify(string? stored, string supplied)
        {
            if (string.IsNullOrEmpty(stored)) return false;
            if (!LooksHashed(stored)) return stored == supplied;
            return Hasher.VerifyHashedPassword(new UserAccount(), stored, supplied) != PasswordVerificationResult.Failed;
        }

        public static string? Validate(string? password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return "Password must be at least 8 characters.";
            if (!password.Any(char.IsLetter) || !password.Any(char.IsDigit))
                return "Password must contain at least one letter and one number.";
            return null;
        }
    }
}
