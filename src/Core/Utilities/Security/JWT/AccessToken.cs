using System;
using Core.Entity.Concretes;

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