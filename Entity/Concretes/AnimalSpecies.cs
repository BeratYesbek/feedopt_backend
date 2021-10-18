using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.concretes
{
    public class AnimalSpecies : IEntity
    {
        [Key] public int AnimalSpeciesId { get; set; }

        [StringLength(50)] public string Kind { get; set; }

        [ForeignKey("AnimalCategoryId")] 
        public virtual AnimalCategory AnimalCategory { get; set; }

        public int AnimalCategoryId { get; set; }

        [JsonIgnore] public virtual ICollection<MissingDeclaration> MissingDeclarations { get; set; }
    }
}