using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Core.Entity.Abstracts;
using Core.Entity.Concretes;
using Entity.Concretes.Translations;


namespace Entity.concretes
{
    public class AnimalCategory : IEntity
    {
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string AnimalCategoryName { get; set; }

        private ICollection<AnimalCategoryTranslation> _AnimalCategoryTranslations;
        public virtual ICollection<AnimalCategoryTranslation> AnimalCategoryTranslations { get; set; }


    }
}