using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Newtonsoft.Json;

namespace Entity.Concretes
{
    public class Age : IEntity
    {
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string AgeRange{ get; set; }
    }
}
