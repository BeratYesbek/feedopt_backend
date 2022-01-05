using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Core.Utilities.Language;

namespace Entity.Dtos
{
    public class UserForRegisterDto : IDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public PreferredLanguage PreferredLanguage { get; set; }

    }
}
