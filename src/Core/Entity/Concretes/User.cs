using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using Core.Entity.Abstracts;
using Core.Utilities.Language;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace Core.Entity.Concretes
{

    public class User : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        
        public bool Status { get; set; } = false;
        public PreferredLanguage PreferredLanguage { get; set; } = PreferredLanguage.tr;
        public string ImagePath { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public IFormFile File { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public byte[] PasswordSalt { get; set; }
    }
}