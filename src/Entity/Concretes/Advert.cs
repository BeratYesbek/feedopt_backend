using System;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public enum Status
    {

        Pending,
        Active,
        Deactivate,

    }

    public enum AdvertCase
    {
        Nothing,
        Adopted,
        Found
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

        public AdvertCase AdvertCase { get; set; } = AdvertCase.Nothing;

        public Status Status { get; set; } = Status.Pending;

        public DateTime CreatedAt { get; set; } = default;

        public DateTime UpdatedAt { get; set; } = default;
        
    }
}
