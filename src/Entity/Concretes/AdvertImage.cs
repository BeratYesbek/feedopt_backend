using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class AdvertImage : IEntity
    {
        public int Id { get; set; }
        public int AdvertId { get; set; }
        public string ImagePath { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Advert Advert { get; set; }
    }
}
