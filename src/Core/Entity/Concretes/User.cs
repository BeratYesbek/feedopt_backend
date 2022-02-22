﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using Core.Entity.Abstracts;
using Core.Utilities.Language;
using Microsoft.AspNetCore.Http;


namespace Core.Entity
{

    public class User : IEntity
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; } = false;

        public bool TwoFactorEnabled { get; set; } = false;

        public bool EmailConfirmed { get; set; } = false;

        public bool Status { get; set; } = false;

        public PreferredLanguage PreferredLanguage { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile File { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public byte[] PasswordSalt { get; set; }
    }
}