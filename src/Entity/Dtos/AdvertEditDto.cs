using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Entity.concretes;
using Entity.Concretes;

namespace Entity.Dtos
{
    public class AdvertEditDto : IDto
    {
        public Advert Advert { get; set; }
        public Color Color { get; set; }
        public AdvertCategory AdvertCategory { get; set; }
        public AnimalCategory AnimalCategory { get; set; }
        public Age Age { get; set; }
        public Location  Location { get; set; }
        public AnimalSpecies AnimalSpecies { get; set; }
        public Status Status  { get; set; }
        public Gender Gender { get; set; }
        public AdvertImage[] AdvertImage { get; set; }
    }
}
