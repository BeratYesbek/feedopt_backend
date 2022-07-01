using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Core.Utilities.Language;
using Entity.concretes;

namespace Entity.Concretes.Translations
{
    public class AnimalCategoryTranslation : IEntity, ITranslation
    {
        public int Id { get; set; }
        public string CultureName { get; set; }
        public string PropertyName { get; set; }
        public string Content { get; set; }
        public int AnimalCategoryId  { get; set; }
        public virtual AnimalCategory AnimalCategory { get; set; }
    }
}