using System.Collections.Generic;
using Core.Entity.Abstracts;
using Core.Utilities.Language;
using Entity.Concretes.Translations;
using System.ComponentModel.DataAnnotations;

namespace Entity.Concretes
{
    public class Color : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Hex { get; set; }

        private ICollection<ColorTranslation> _colorTranslations;

        public virtual ICollection<ColorTranslation> ColorTranslations
        {
            get
            {
                if (_colorTranslations != null)
                    new Translate<ColorTranslation>().TranslateProperties(_colorTranslations, this);
                return null;
            }
            set => _colorTranslations = value;
        }



    }
}
