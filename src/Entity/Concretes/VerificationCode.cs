using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public enum CodeType
    {
        ResetPassword
    }
    public class VerificationCode : IEntity
    {
        public int Id { get; set; }
        public string CodeHash { get; set; }
        public string Email { get; set; }
        public DateTime Expiry { get; set; }
        public CodeType Type { get; set; }
    }
}
