using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Entity.Abstracts;
using Entity.concretes;
using Entity.Concretes;

namespace Entity
{
    public class MissingDeclaration : IEntity
    {
        [Key]
        public int MissingDeclarationId { get; set; }

        public string Description { get; set; }

        public int AnimalSpeciesId { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("AnimalSpeciesId")]
        public virtual AnimalSpecies AnimalSpecies { get; set; }

        public virtual ICollection<MissingDeclarationImage> MissingDeclarationImages { get; set; }
    }
}