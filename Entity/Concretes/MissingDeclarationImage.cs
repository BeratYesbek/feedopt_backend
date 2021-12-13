using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class MissingDeclarationImage : IEntity
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public string PublicId { get; set; }


        public int MissingDeclarationId { get; set; }
    }
}