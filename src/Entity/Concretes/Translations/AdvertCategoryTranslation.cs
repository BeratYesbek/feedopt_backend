using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Core.Utilities.Language;

namespace Entity.Concretes.Translations
{
    public class AdvertCategoryTranslation : IEntity,ITranslation
    {
        public int Id { get; set; }
        public string CultureName { get; set; }
        public string PropertyName { get; set; }
        public string Content { get; set; }
        public int AdvertCategoryId { get; set; }
        public virtual AdvertCategory AdvertCategory { get; set; }
    }
}
