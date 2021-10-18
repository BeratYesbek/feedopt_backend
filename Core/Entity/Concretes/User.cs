﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entity.Abstracts;
using Newtonsoft.Json;

namespace Core.Entity
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }
    }
}