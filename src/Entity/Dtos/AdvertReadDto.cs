using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Entity.Abstracts;
using Core.Entity.Concretes;
using Entity.concretes;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Entity.Dtos
{
    public class AdvertReadDto : IDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int AnimalSpeciesId { get; set; }
        public int AdvertCategoryId { get; set; }
        public int AnimalCategoryId { get; set; } = 1;
        public int LocationId { get; set; }
        public string AnimalName { get; set; }
        public int ColorId { get; set; }
        public int AgeId { get; set; }
        public double Distance { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool FavoriteStatus { get; set; } = false;
        public User User { get; set; }
        public AnimalSpecies AnimalSpecies { get; set; }
        public Age Age { get; set; }
        public Color Color { get; set; }
        
        private FavoriteAdvert _favoriteAdvert;
        public FavoriteAdvert FavoriteAdvert
        {
            get => _favoriteAdvert;
            set
            {
                _favoriteAdvert = value;
                if (value != null)
                    FavoriteStatus = true;
            }
        }
        public string[] Images { get; set; }
        
        public AnimalCategory AnimalCategory { get; set; }

        public AdvertCategory AdvertCategory { get; set; }

        public Location Location { get; set; }

        public AdvertImage[] AdvertImages { get; set; }
    }
}
