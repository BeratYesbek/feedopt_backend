using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Concretes;

namespace Core.Utilities.Language
{
    public class TranslationCollection<TEntity>
    {
        public TEntity Entity { get; set; }

        public TranslationCollection(TEntity entity,Translation translation) 
        {
            Entity = entity;

            var propertyInfo = entity.GetType().GetProperty(nameof(Translation));
            propertyInfo?.SetValue(entity,translation);
        }
    }
}
