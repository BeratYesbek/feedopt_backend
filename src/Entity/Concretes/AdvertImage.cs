using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Core.Utilities.Cloud.Cloudinary;
using Microsoft.AspNetCore.Http;
namespace Entity.Concretes
{
    public class AdvertImage : Cloudinary, IEntity
    {
        public int Id { get; set; }

        public int AdvertId { get; set; }
    }
}
