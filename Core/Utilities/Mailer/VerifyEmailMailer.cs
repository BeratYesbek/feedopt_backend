using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Core.Entity;
using FluentEmail.Core;
using RazorLight.Extensions;

namespace Core.Utilities.Mailer
{
    public class VerifyEmailMailer
    {
        private static readonly string defaultEmail = "beratyesbek@gmail.com";

        public static void SendVerifyEmail(string subject, string email)
        {
            var smtp = new SmtpSender(() => new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = defaultEmail,
                    Password = "pstezzouzcdckpem"
                }
            });

            Email.DefaultSender = smtp;
            Email.DefaultRenderer = new RazorRenderer();

            var template = "Dear @Model.Name, You are totally @Model.Compliment.";
            User user = new User();
            user.FirstName = "Berat Yesbek"; 
            Email.From(defaultEmail)
                .To(email)
                .Subject(subject)
                //.UsingTemplate(template, new {Name = "Luke", Compliment = "Awesome"}).Send();
               .UsingTemplateFromFile($"{Environment.CurrentDirectory}\\wwwroot\\static\\email\\VerifyEmail.cshtml",
                    new {User = user}).Send();
        }
    }
}