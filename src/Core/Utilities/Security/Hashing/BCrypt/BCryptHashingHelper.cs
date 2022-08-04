
namespace Core.Utilities.Security.Hashing.BCrypt
{
    public class BCryptHashingHelper
    {
        public static string HashValue(string value)
        {
           return global::BCrypt.Net.BCrypt.HashPassword(value);
        }

        public static bool VerifyHashValue(string value,string hashValue)
        {
            return global::BCrypt.Net.BCrypt.Verify(value, hashValue);
        }
    }
}
