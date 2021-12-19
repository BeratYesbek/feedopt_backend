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
    public static class VerifyEmailMailer
    {
        private static readonly string _currentDirectory = $"{Environment.CurrentDirectory}\\wwwroot\\static\\mailer\\";
        private const string _verifyHtmlPage = "VerifyEmail.cshtml";

        public static async Task SendVerifyEmail(User user, string subject)
        {
            var _email = Mailer.StartMailer(subject, user.Email);
            await _email.UsingTemplateFromFile($"{_currentDirectory}{_verifyHtmlPage}", new { User = user }).SendAsync();
        }
    }
}