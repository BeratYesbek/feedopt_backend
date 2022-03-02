﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Entity.Abstracts;
using Entity.concretes;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Entity.Dtos
{
    public class AdvertReadDto : IDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int AnimalSpeciesId { get; set; }

        public int AdvertCategoryId { get; set; }

        public string AdvertCategoryName { get; set; }

        public string Kind { get; set; }

        public string AnimalCategoryName { get; set; }

        public string AnimalName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string[] Images { get; set; }

        /// <summary>
        ///   Location Data
        /// </summary>
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string County { get; set; }

        public int Distance { get; set; }





        public User User { get; set; }

        public AnimalSpecies AnimalSpecies { get; set; }

        public AnimalCategory AnimalCategory { get; set; }

        public AdvertCategory AdvertCategory { get; set; }

        public Location Location { get; set; }

        public AdvertImage[] AdvertImages { get; set; }
    }
}
