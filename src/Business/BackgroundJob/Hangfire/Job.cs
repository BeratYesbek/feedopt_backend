using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BackgroundJob.Hangfire
{
    public static class Job 
    {
        public static T Create<T>() where T : class, IActiveJob
        {
            return (T) Activator.CreateInstance(typeof(T));
        }
    }
}
