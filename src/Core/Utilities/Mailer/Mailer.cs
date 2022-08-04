using FluentEmail.Razor;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;
using FluentEmail.Core;

namespace Core.Utilities.Mailer
{
    public class Mailer
    {

        public static string EmailOption { get; set; }
        public static string PasswordOption { get; set; }
        public static string HostOption { get; set; }
        public static int PortOption { get; set; }

        public static IFluentEmail StartMailer(string subject, string email)
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