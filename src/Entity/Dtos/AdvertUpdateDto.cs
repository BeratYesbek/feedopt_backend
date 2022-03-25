using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Entity.Dtos
{
    public class AdvertUpdateDto : IDto
    {
        public int Id { get; set; }

        public string Description { get; set; }


        public int AnimalSpeciesId { get; set; }

        public int AdvertCategoryId { get; set; }

        public string AnimalName { get; set; }

        public Age Age { get; set; }

        public Gender Gender { get; set; }

        public string Color { get; set; }

        public IFormFile[] Files { get; set; }

        /// <summary>
        ///   Location Data
        /// </summary>

        public int LocationId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string County { get; set; }
    }
}
