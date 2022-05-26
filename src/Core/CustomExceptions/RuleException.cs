using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
