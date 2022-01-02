using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Utilities.Mailer;
using FluentEmail.Core.Models;

namespace Business.BusinessMailer
{
    public class VerifyEmailMailer : IMailer
    {
        private static readonly string _currentDirectory = $"{Environment.CurrentDirectory}\\wwwroot\\static\\mailer\\";
        private const string _verifyHtmlPage = "VerifyEmail.cshtml";

        public void SendEmail(EmailType emailType, User user)
        {
            switch (emailType)
            {
                case EmailType.VerifyEmail:
                    SendVerifyEmail(user, "Email Verification");
                    break;
                default:
                    throw new ArgumentException("This is not suitable type for this class");

            }
        }

        public  async void SendVerifyEmail(User user, string subject)
        {
            var _email = Mailer.StartMailer(subject, user.Email);
            await _email.UsingTemplateFromFile($"{_currentDirectory}{_verifyHtmlPage}", new { User = user }).SendAsync();
        }

      
    }
}