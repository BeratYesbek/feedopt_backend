using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity;
using Core.Entity.Abstracts;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Entity.concretes
{

    public class AdoptionNotice : Animal, IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }



        public int AnimalSpeciesId { get; set; }

        [NotMapped]
        public IFormFile[] FormFiles { get; set; }

    }
}