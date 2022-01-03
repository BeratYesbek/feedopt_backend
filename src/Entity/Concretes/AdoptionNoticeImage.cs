using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Entity.concretes;

namespace Entity.Concretes
{
    public class AdoptionNoticeImage : IEntity
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public string PublicId { get; set; }

        public int AdoptionNoticeId { get; set; }

        public AdoptionNoticeImage(int id, string imagePath, string publicId, int adoptionNoticeId) : this(imagePath, publicId, adoptionNoticeId)
        {

            Id = id;

        }

        public AdoptionNoticeImage(string imagePath, string publicId, int adoptionNoticeId)
        {
            ImagePath = imagePath;
            PublicId = publicId;
            AdoptionNoticeId = adoptionNoticeId;
        }

        public AdoptionNoticeImage()
        {
            
        }
    }
}