using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Entity.concretes;

namespace Entity.Concretes
{
    public class Location : IEntity
    {
        [Key] public int LocationId { get; set; }

        [StringLength(500)] 
        public string Address { get; set; }

        [StringLength(100)] 
        public string City { get; set; }

        [StringLength(100)] 
        public string Country { get; set; }

        [StringLength(500)] 
        public string PlaceId { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [JsonIgnore] 
        public virtual ICollection<MissingDeclaration> MissingDeclarations { get; set; }

        [JsonIgnore] 
        public virtual ICollection<AdoptionNotice> AdoptionNotices { get; set; }
    }
}