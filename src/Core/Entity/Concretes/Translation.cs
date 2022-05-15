using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Language;

namespace Core.Entity.Concretes
{
    public abstract class Translation<T> where T : Translation<T>, new()
    {
        public int Id { get; set; }

        public string CultureName { get; set; }


    }
}