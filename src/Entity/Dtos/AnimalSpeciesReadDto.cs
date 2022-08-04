using Core.Entity.Abstracts;


namespace Entity.Dtos
{
    public class AnimalSpeciesReadDto : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AnimalCategoryId { get; set; }
    }
}
