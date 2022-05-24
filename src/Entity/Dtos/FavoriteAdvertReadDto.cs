using System;
using Core.Entity;
using Core.Entity.Abstracts;
using Entity.concretes;
using Entity.Concretes;

namespace Entity.Dtos
{
    public class FavoriteAdvertReadDto : IDto
    {
        public int Id { get; set; }
        
        public int AdvertId { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int AnimalSpeciesId { get; set; }

        public int AdvertCategoryId { get; set; }

        public int ColorId { get; set; }

        public int AgeId { get; set; }

        public string AdvertCategoryName { get; set; }

        public string Kind { get; set; }

        public string AnimalCategoryName { get; set; }

        public string AnimalName { get; set; }


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

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


        public User User { get; set; }

        public AnimalSpecies AnimalSpecies { get; set; }

        public AnimalCategory AnimalCategory { get; set; }

        public AdvertCategory AdvertCategory { get; set; }

        public Location Location { get; set; }

        public Color Color { get; set; }

        public Age Age { get; set; }

        public AdvertImage[] AdvertImages { get; set; }


    }
}