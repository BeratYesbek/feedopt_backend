using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class UserLocation : IEntity
    {
        public int Id { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
