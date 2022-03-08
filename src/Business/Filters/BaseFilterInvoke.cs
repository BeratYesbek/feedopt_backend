using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Filters
{
    public class BaseFilterInvoke
    {
        protected virtual object GetInvokeMethod(string methodName, object[] methodParams, object reference = null)
        {
            reference ??= this;
            var method = this.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return method?.Invoke(reference, methodParams);
        }

    }
}
