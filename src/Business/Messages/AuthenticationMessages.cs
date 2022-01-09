using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public class AuthenticationMessages
    {
        public static string AuthenticationFailedMessage = "You are not authenticated. You need to sign in";
        public static string AuthorizationFailedMessage = "You have no authorization";
        public static string TokenExpire = "Token expire is up";
    }
}