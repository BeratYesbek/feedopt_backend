using System;
using System.Net;
using System.Net.Mail;
using Core.Entity.Concretes;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Mailer.FluentMailer
{
    public class FluentMailer : IFluentMailer
    {
        public  string EmailOption { get; set; }
        public  string PasswordOption { get; set; }
        public  string HostOption { get; set; }
        public  int PortOption { get; set; }

        public FluentMailer(IConfiguration configuration)
        {
            var section = configuration.GetSection("Mailer");
            EmailOption = section["Email"];
            PasswordOption = section["Password"]; 
            HostOption = section["Host"];
            PortOption = Convert.ToInt32(section["Port"]);
        }
        public IFluentEmail StartMailer(string subject, string email)
        {
            var smtp = new SmtpSender(() => new SmtpClient()
            {
                Host = HostOption,
                Port = PortOption,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = EmailOption,
                    Password = PasswordOption
                }
            });

            Email.DefaultSender = smtp;
            Email.DefaultRenderer = new RazorRenderer();

            var fluentEmail = Email.From(EmailOption)
                .To(email)
                .Subject(subject);
            return fluentEmail;
        }
    }
}
