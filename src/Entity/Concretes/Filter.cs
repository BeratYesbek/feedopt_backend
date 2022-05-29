using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity.Abstracts;
using Core.Entity.Concretes;
using Newtonsoft.Json;

namespace Entity.Concretes
{
    public class Filter : IEntity
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Type { get; set; }

        public string InputType { get; set; }

        public string DataType { get; set; }

        public string FilterType { get; set; }


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
