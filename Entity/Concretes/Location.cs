using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Entity.concretes;

namespace Entity.Concretes
{
    public class Location : IEntity
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PlaceId { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}