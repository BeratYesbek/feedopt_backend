using Core.Entity.Abstracts;
using Entity.concretes;
using Entity.Concretes;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class OptionDto : IDto
    {
        public List<Color> Color { get; set; }

        public List<AgeRangesReadDto> Age { get; set; }

        public List<AdvertCategory> AdvertCategory { get; set; }

        public List<AnimalCategoryReadDto> AnimalCategory { get; set; }


        public object[] Gender { get; set; }

    }
}
