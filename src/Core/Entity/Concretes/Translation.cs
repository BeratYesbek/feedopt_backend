using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Core.Utilities.Language;

namespace Core.Entity.Concretes
{
    public class Translation : IEntity
    {
        public int Id { get; set; }

        public string Type { get; set; }
        
        public string CultureName { get; set; }

        public string PropertyName { get; set; }

        public int TypeId { get; set; }

        public string Content { get; set; }


    }
}