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
    public class Mailer
    {
        private static readonly string DefaultEmail = "beratyesbek@gmail.com";


        public static IFluentEmail StartMailer(string subject, string email)
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
                    UserName = DefaultEmail,
                    Password = "............................"
                }
            });

            Email.DefaultSender = smtp;
            Email.DefaultRenderer = new RazorRenderer();

           var fluentEmail = Email.From(DefaultEmail)
                .To(email)
                .Subject(subject);
           return fluentEmail;
        }
    }
}