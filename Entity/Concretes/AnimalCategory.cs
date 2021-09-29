using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Newtonsoft.Json;

namespace Entity.concretes
{
    public class AnimalCategory : IEntity
    {
        [Key] 
        public int AnimalCategoryId { get; set; }

        [StringLength(50)] 
        public string AnimalCategoryName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<AnimalSpecies> AnimalSpecies { get; set; }
    }
}