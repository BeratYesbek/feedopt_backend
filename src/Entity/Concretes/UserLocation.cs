﻿using System;
using Core.Entity.Abstracts;
using Core.Entity.Concretes;

namespace Entity.Concretes
{
    public class UserLocation : IEntity
    {
        public int Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User User { get; set; }
    }
}
