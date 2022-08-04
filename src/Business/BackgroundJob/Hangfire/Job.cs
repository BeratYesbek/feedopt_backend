using System;

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
