
using Core.Entity.Concretes;

namespace Core.Utilities.Mailer
{
    public interface IMailer<out T>
    {
        public T StartMailer(string subject,string email);
    }
}
