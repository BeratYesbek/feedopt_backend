using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Utilities.Mailer
{
    public interface IMailer<out T>
    {
        public void SendEmail(EmailType emailType, User user);

        public T StartMailer(string subject,string email);
    }
}
