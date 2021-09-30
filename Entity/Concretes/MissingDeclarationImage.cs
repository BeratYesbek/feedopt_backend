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
        [Key] public int MissingDeclarationImageId { get; set; }

        public string ImagePath { get; set; }

        public int MissingDeclarationId { get; set; }

        [ForeignKey("MissingDeclarationId")]
        public virtual MissingDeclaration MissingDeclaration { get; set; }
    }
}