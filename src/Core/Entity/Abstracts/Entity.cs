using Core.Entity.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entity.Abstracts
{
    public abstract class Entity
    {

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
