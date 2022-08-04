
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class Location : IEntity
    {
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string County { get; set; }
    }
}