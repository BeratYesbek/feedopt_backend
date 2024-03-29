﻿using Core.Entity.Abstracts;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entity.Dtos
{
    [Bind]
    public class AdvertCreateDto : IDto
    {
        public string Description { get; set; }

        public int UserId { get; set; }

        public int AnimalSpeciesId { get; set; }

        public int AdvertCategoryId { get; set; }

        public int AnimalCategoryId { get; set; }

        public int ColorId { get; set; }

        public string AnimalName { get; set; }

        public int AgeId { get; set; }

        public Gender Gender { get; set; }

        public Status Status { get; set; }

        public IFormFile[] Files { get; set; }

        /// <summary>
        ///   Location Data
        /// </summary>
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string County { get; set; }

    }
}
