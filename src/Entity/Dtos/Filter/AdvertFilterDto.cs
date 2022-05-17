using Core.Entity.Abstracts;
using Entity.Concretes;

namespace Entity.Dtos.Filter
{
    public class AdvertFilterDto : IDto
    {
        public int UserId { get; set; } = default;

        public int[] AnimalSpeciesId { get; set; }

        public int[] AdvertCategoryId { get; set; }

        public int[] AnimalCategoryId { get; set; }

        public int[] ColorId { get; set; }

        public int[] AgeId { get; set; }

        public Gender[] Gender { get; set; }

    }
}
