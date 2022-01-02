using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Utilities.Mailer
{
    public interface IMailer
    {
        public void SendEmail(EmailType emailType, User user);
    }
}
