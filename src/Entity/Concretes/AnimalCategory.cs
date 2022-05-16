using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity.Abstracts;
using Core.Entity.Concretes;
using Newtonsoft.Json;


namespace Entity.concretes
{
    public class AnimalCategory : IEntity
    {
        public int Id { get; set; }

        public string AnimalCategoryName { get; set; }


        [NotMapped]
        [JsonIgnore]
        public Translation Translation
        {
            get => null;

            set
            {
                if (value != null)
                {
                    var property = GetType().GetProperty(value.PropertyName);
                    property?.SetValue(this, value.Content);
                }
            }
        }
    }
}