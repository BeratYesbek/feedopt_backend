using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public enum Status
    {

        Pending,
        Active,
        Deactivate,

    }
    
    public class Advert : Animal, IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int AnimalSpeciesId { get; set; }

        public int AdvertCategoryId { get; set; }

        public int LocationId { get; set; }

        public Status Status { get; set; } = Status.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
