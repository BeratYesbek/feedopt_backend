using System.Text.Json.Serialization;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class Age : IEntity
    {
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string AgeRange{ get; set; }
    }
}
