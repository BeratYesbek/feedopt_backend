using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Security.JWT;

namespace Core.Entity.Concretes
{
    public class CookieParams
    {
        public AccessToken AccessToken { get; set; }

        public User User { get; set; }
    }
}
