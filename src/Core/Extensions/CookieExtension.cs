

using System.Net;

namespace Core.Extensions
{
    public static class CookieExtension
    {
        public static void SecureHttpOnly( string key, string value)
        {
            Cookie cookie = new Cookie(key, value);
           cookie.HttpOnly = true;
           cookie.Secure = true;
        }

        public static void WithoutSecureHttpOnly(string key, string value)
        {
            Cookie cookie = new Cookie(key,value);
            cookie.HttpOnly = false;
            cookie.Secure = false;
        }
    }
}