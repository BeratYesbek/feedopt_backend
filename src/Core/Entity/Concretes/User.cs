using System;
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

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool Status { get; set; }

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
    }
}