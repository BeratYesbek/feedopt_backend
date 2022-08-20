using System.Collections.Generic;
using Core.Entity.Abstracts;
using Core.Utilities.Language;
using Entity.Concretes.Translations;

namespace Entity.Concretes
{
    public class AdvertCategory : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Advert> Adverts { get; set; }

        /* private ICollection<AdvertCategoryTranslation> _advertCategoryTranslations;
 
         public virtual ICollection<AdvertCategoryTranslation> AdvertCategoryTranslations
         {
             get
             {
                 if(_advertCategoryTranslations != null )
                     new Translate<AdvertCategoryTranslation>().TranslateProperties(_advertCategoryTranslations,this);
                 return null;
             }
             set => _advertCategoryTranslations = value;
         }*/

    }
}
