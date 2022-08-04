using System;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class FavoriteAdvert : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AdvertId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}