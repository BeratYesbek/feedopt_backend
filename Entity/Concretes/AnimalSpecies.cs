using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.concretes
{
    public class AnimalSpecies : IEntity
    {
        [Key]
        public int AnimalSpeciesId { get; set; }

        [StringLength(50)]
        public string Kind { get; set; }

        public int AnimalCategoryId { get; set; }

        public virtual AnimalCategory AnimalCategory { get; set; }

    }
}