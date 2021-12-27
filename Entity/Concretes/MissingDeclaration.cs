using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity;
using Core.Entity.Abstracts;
using Entity.concretes;
using Entity.Concretes;

namespace Entity
{
    public class MissingDeclaration : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int AnimalId { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }
    }
}