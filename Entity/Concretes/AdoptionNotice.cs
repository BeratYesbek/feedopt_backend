using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Entity.Abstracts;

namespace Entity.concretes
{
    public class AdoptionNotice : IEntity
    {
        public int AdoptionNoticeId { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public string City { get; set; }

        public long Lan { get; set; }

        public long Lon { get; set; }

        public int AnimalSpeciesId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("AnimalSpeciesId")]
        public virtual AnimalSpecies AnimalSpecies { get; set; }
    }
}