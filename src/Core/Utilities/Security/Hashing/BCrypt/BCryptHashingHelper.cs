using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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
