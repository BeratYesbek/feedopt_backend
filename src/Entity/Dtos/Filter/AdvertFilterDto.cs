using Core.Entity.Abstracts;

namespace Entity.Dtos.Filter
{
    public class AdvertFilterDto : IDto
    {
        public int UserId { get; set; } = default;

        public int[] AnimalSpeciesId { get; set; }

        public int[] AdvertCategoryId { get; set; }

        public int[] AnimalCategoryId { get; set; }

        public string[] AnimalSpeciesName { get; set; }

        public string[] AdvertCategoryName { get; set; }

        public string[] AnimalCategoryName { get; set; }

    }
}
