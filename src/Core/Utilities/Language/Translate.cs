using Core.Entity.Abstracts;
using Core.Entity.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Language
{
    public  class Translate<T> where T : class,ITranslation
    {
        public void TranslateProperties(ICollection<T> collection,IEntity entity)
        {
            var translations = collection.Where(t => t.CultureName == CurrentUser.CultureName).ToList();

            translations.ForEach(item =>
            {
                var property = entity.GetType().GetProperty(item.PropertyName);
                property?.SetValue(entity, item.Content);
            });
            
        }
    }
}
