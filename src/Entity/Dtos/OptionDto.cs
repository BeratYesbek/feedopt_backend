using Core.Entity.Abstracts;
using Entity.concretes;
using Entity.Concretes;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class OptionDto : IDto
    {
        public List<Color> Color { get; set; }

        public List<Age> Age { get; set; }

        public List<AdvertCategory> AdvertCategory { get; set; }

        public List<AnimalCategory> AnimalCategory { get; set; }

        public List<Gender> Gender { get; set; }

    }
}
