using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
    public static class AppDbContextExtension
    {
        public static IQueryable SetType(this AppDbContext context, Type T)
        {
            // Get the generic type definition
            MethodInfo method = typeof(AppDbContext).GetMethod(nameof(AppDbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            // Build a method with the specific type argument you're interested in
            method = method.MakeGenericMethod(T);

            return method.Invoke(context, null) as IQueryable;
        }


    }
}
