using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class Age : IEntity
    {
        public int Id { get; set; }
        public string AgeRange{ get; set; }
        public List<Advert> Adverts { get; set; }
    }
}
