using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Entity.Concretes
{
    public class Ticket : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public IFormFile[] FormFiles { get; set; }
    }
}