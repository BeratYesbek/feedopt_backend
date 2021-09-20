using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.concretes
{
    public class AnimalCategory : IEntity
    {
        [Key]
        public int AnimalCategoryId { get; set; }

        [StringLength(50)]
        public string AnimalCategoryName { get; set; }

        public ICollection<AnimalSpecies> AnimalSpecies { get; set; }
    }
}