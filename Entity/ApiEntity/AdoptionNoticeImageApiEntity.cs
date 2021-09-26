using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.concretes;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Entity.ApiEntity
{
    /// <summary>
    ///  purpose of this class if formData coming from api it will come to be formFile.
    /// we cannot give being parameter non IEntity to business layer .
    /// we have to give IEntity class for best practices.
    /// </summary>
    public class AdoptionNoticeImageApiEntity
    {
        public AdoptionNoticeImage[] AdoptionNoticeImage { get; set; }

        public IFormFile[] FormFiles { get; set; }
    }
}
