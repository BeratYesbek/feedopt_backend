using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.Dtos.Filter
{
    public class AdvertFilterDto : IDto
    {
        public int UserId { get; set; } = default;

        public int[] AnimalSpeciesId { get; set; }

        public int[] AdvertCategoryId { get; set; }

        public int[] AnimalCategoryId { get; set; }

    }
}
