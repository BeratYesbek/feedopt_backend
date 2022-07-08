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
        public int AnimalCategoryId { get; set; }
        public string AnimalName { get; set; }
        public int ColorId { get; set; }
        public int AgeId { get; set; }
        public Gender Gender { get; set; }
        public IFormFile[] Files { get; set; }
        /// <summary>
        ///   Location Data
        /// </summary>
        public int LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public int UserId { get; set; }
        public int[] DeletedImages { get; set; }
    }
}
