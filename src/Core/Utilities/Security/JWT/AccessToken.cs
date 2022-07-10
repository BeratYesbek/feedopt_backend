using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Utilities.Security.JWT
{
    public enum TokenType
    {
        Standard,
        ResetPassword
    }
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public User User { get; set; }
    }
}