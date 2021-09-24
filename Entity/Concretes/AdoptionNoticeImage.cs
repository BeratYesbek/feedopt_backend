using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Entity.concretes;

namespace Entity.Concretes
{
    public class AdoptionNoticeImage : IEntity
    {
        [Key]
        public int AdoptionNoticeImageId { get; set; }

        public string ImagePath { get; set; }

        public int AdoptionNoticeId { get; set; }

        [ForeignKey("AdoptionNoticeId")]
        public virtual AdoptionNotice AdoptionNotice { get; set; }
    }
}