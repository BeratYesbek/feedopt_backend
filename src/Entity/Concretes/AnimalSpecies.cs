using Core.Entity.Abstracts;

namespace Entity.concretes
{
    public class AnimalSpecies : IEntity
    {
        public int Id { get; set; }

        public string Kind { get; set; }

        public int AnimalCategoryId { get; set; }
    }
}