using Core.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos
{
    public class AnimalSpeciesReadDto : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AnimalCategoryId { get; set; }
    }
}
