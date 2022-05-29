﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Core.Entity.Abstracts;
using Core.Entity.Concretes;


namespace Entity.concretes
{
    public class AnimalCategory : IEntity
    {
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string AnimalCategoryName { get; set; }


     /*   [NotMapped]
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
        }*/

      
    }
}