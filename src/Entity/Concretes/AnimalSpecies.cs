using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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