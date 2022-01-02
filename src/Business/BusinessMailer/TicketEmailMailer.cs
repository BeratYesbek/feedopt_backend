using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aspects.Autofac;
using Core.Entity;
using Core.Utilities.Mailer;

namespace Business.BusinessMailer
{
    public class TicketEmailMailer : IMailer
    {
        private static readonly string _currentDirectory = $"{Environment.CurrentDirectory}\\wwwroot\\static\\mailer\\";
        private const string _ticketEmailHtmlPage = "VerifyEmail.cshtml";


        public void SendEmail(EmailType emailType, User user)
        {
            switch (emailType)
            {
                case EmailType.TicketAnswer:
                    SendAnswerEmail(user, "Ticket Answer");
                    break;
                case EmailType.TicketEmail:
                    InfoTicketEmail(user, "Ticket Info");
                    break;
                default:
                    throw new ArgumentException("This is not suitable type for this class");
            }
        }

        private async void SendAnswerEmail(User user, string subject)
        {
            var _email = Mailer.StartMailer(subject, user.Email);
            await _email.UsingTemplateFromFile($"{_currentDirectory}{_ticketEmailHtmlPage}", new { User = user })
                .SendAsync();
        }

        private async void InfoTicketEmail(User user, string subject)
        {
            var _email = Mailer.StartMailer(subject, user.Email);
            await _email.UsingTemplateFromFile($"{_currentDirectory}{_ticketEmailHtmlPage}", new { User = user })
                .SendAsync();
        }
    }
}