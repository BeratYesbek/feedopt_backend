using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
