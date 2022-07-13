
using Core.Entity.Concretes;

namespace Core.Utilities.Mailer
{
    public interface IMailer<out T>
    {
        public void SendEmail(EmailType emailType, User user);

        public T StartMailer(string subject,string email);
    }
}
