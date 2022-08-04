using System;

namespace Core.CustomExceptions
{
    public class AuthenticationFailedException : Exception
    {
        public AuthenticationFailedException()
        {
            
        }

        public AuthenticationFailedException(string message) : base(message)
        {
            
        }
    }
}
