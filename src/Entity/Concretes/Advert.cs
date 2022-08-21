using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Core.Entity.Abstracts;
using Core.Entity.Concretes;
using Core.Utilities.Calculator;
using Entity.concretes;

namespace Entity.Concretes
{
    public enum Status
    {
        Pending,
        Active,
        Deactivate,
    }

    public class Advert : Animal, IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int AnimalSpeciesId { get; set; }
        public int AdvertCategoryId { get; set; }
        public int AnimalCategoryId { get; set; } = 1;
        public int LocationId { get; set; }
        public bool IsDeleted { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public AnimalCategory AnimalCategory { get; set; }
        public AnimalSpecies AnimalSpecies { get; set; }
        public AdvertCategory AdvertCategory { get; set; }
        public Color Color { get; set; }
        public Age Age { get; set; }
        public User User { get; set; }
        private Location _location;
        public Location Location
        {
            get
            {
                Distance = (int)Calculator.CalculateDistance(CurrentUser.Latitude, CurrentUser.Longitude, _location.Latitude, _location.Longitude);
                return _location;
            }
            set
            {
                _location = value;
            }
            
        }
        public List<AdvertImage> AdvertImages { get; set; }
        public List<FavoriteAdvert> FavoriteAdverts { get; set; }

        [NotMapped]
        public int Distance { get; set; }

    }
}
