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
        
        public int UserId { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        
        public Advert Advert { get; set; }
        
        public AdvertCategory AdvertCategory { get; set; }
        
        public AnimalSpecies AnimalSpecies { get; set; }
        
        public AnimalCategory AnimalCategory { get; set; }
        
        public string[] Images { get; set; }
        
    }
}