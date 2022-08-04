using Core.Utilities.Security.JWT;

namespace Core.Entity.Concretes
{
    public class CookieParams
    {
        public AccessToken AccessToken { get; set; }

        public User User { get; set; }
    }
}
