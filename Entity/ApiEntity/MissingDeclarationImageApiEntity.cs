using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Entity.Concretes;
using Entity.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Entity.ApiEntity
{
    /// <summary>
    ///  purpose of this class if formData coming from api it will come to be formFile.
    /// we cannot give being parameter non IEntity to business layer .
    /// we have to give IEntity class for best practices.
    /// /// </summary>
    public class MissingDeclarationImageApiEntity
    {
       
        public string MissingDeclarationImage { get; set; }

        public IFormFile[] FormFiles { get; set; }
    }
}