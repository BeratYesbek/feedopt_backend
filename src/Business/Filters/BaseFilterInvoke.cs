using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Filters
{
    /// <summary>
    /// Purpose of this class run extended methods of classes. Whenever you create a filter class you must inherited base class 
    /// </summary>
    public class BaseFilterInvoke
    {
        /// <summary>
        /// This method run methods by name giving parameters 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="methodParams"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        protected virtual object GetInvokeMethod(string methodName, object[] methodParams, object reference = null)
        {
            reference ??= this;
            var method = this.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return method?.Invoke(reference, methodParams);
        }

    }
}
