using Core.Entity.Abstracts;
using Core.Utilities.Language;

namespace Entity.Concretes.Translations
{
    public class ColorTranslation : IEntity,ITranslation
    {
        public int Id { get; set; }

        public string PropertyName { get; set; }

        public string Content { get; set; }

        public string CultureName { get; set; }

        public int ColorId { get; set; }

        public virtual Color Color { get; set; }
    }
}
