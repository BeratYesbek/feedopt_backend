using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity;
using Core.Entity.Abstracts;
using Entity.Concretes;

namespace Entity.concretes
{
    public class AdoptionNotice : IEntity
    {
        public int AdoptionNoticeId { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }

        public int AnimalSpeciesId { get; set; }

        [ForeignKey("LocationId")] 
        public virtual Location Location { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("AnimalSpeciesId")] 
        public virtual AnimalSpecies AnimalSpecies { get; set; }

        [JsonIgnore] 
        public virtual ICollection<AdoptionNoticeImage> AdoptionNoticeImage { get; set; }
    }
}