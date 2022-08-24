using System.Collections.Generic;
using Core.Entity.Abstracts;
using Entity.Concretes;

namespace Entity.concretes
{
    public class AnimalSpecies : IEntity
    {
        public int Id { get; set; }

        public string Kind { get; set; }
        public int AnimalCategoryId { get; set; }
        public List<Advert> Adverts { get; set; }

        public AnimalCategory AnimalCategory { get; set; }
    }
}