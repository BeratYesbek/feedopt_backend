using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Core.Entity.Abstracts;
using Core.Utilities.Language;
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

        public bool PhoneNumberConfirmed { get; set; } = false;

        public bool TwoFactorEnabled { get; set; } = false;

        public bool EmailConfirmed { get; set; } = false;

        public bool Status { get; set; } = false;

        public PreferredLanguage PreferredLanguage { get; set; }

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
    }
}