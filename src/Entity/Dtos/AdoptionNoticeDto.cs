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
    public class AdoptionNoticeDto : IDto
    {
        public AdoptionNotice AdoptionNotice { get; set; }

        public AdoptionNoticeImage[] AdoptionNoticeImage { get; set; }

        public AnimalSpecies AnimalSpecies { get; set; }
    }

}