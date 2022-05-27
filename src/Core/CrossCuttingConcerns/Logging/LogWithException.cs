using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogWithException : LogDetail
    {
        public string ExceptionMessage { get; set; }
        public Exception Exception { get; set; }

    }
}