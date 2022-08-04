using System;

namespace Core.CustomExceptions
{
    public class RuleException : Exception
    {
        public RuleException()
        {

        }

        public RuleException(string message) : base(message)
        {

        }
    }
}
