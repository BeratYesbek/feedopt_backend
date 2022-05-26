using Business.Abstracts;
using Business.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities
{
    public static class InstanceCreator<T>
    {
        public static T CreateInstance()
        {
           var instance = Collection(typeof(T));
            return (T)Activator.CreateInstance(instance);
        }

        private static Type Collection(Type type)
        {
            Dictionary<object,object> map = new Dictionary<object,object>();
            map.Add(typeof(IUserService), typeof(UserManager));
            var value = map[type];
            return (Type) value;
        }
    }
}
