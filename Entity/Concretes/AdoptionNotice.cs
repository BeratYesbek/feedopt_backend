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

namespace Entity.concretes
{
    public class AdoptionNotice : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }

        public int AnimalId { get; set; }

    }
}