using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Newtonsoft.Json;

namespace Entity.concretes
{
    public class AnimalCategory : IEntity
    {
        public int Id { get; set; }

        public string AnimalCategoryName { get; set; }
    }
}