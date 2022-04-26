using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Core.Utilities.Cloud.Cloudinary;
using Core.Utilities.Mailer;
using DataAccess;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Config
{
    public class Config : IConfig
    {
        private IConfiguration Configuration { get; }
        private IServiceProvider ServiceProvider { get; }

        public Config(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            ServiceProvider = serviceProvider;
        }

        public void Run()
        {
            //DatabaseMigration();
            SetMailerOptions();
        }

        private void SetMailerOptions()
        {
            IConfigurationSection section = Configuration.GetSection("Mailer");
            Mailer.EmailOption = section["Email"];
            Mailer.PasswordOption = section["Password"];
            Mailer.HostOption = section["Host"];
            Mailer.PortOption = Convert.ToInt32(section["Port"]);

            //var smtp = new SmtpSender(() => new SmtpClient()
            //{
            //    Host = section["Host"],
            //    Port = Convert.ToInt32(section["Port"]),
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential()
            //    {
            //        UserName = section["Email"],
            //        Password = section["Password"]
            //    }
            //});

            //Email.DefaultSender = smtp;
            //Email.DefaultRenderer = new RazorRenderer();

            //var fluentEmail = Email.From("dontreply@feedopt.com")
            //    .To("beratyesbek@gmail.com")
            //    .Subject("Test").Send();
            //Debug.WriteLine(fluentEmail.ErrorMessages.ToString());
        }

        private void DatabaseMigration()
        {
            ConnectionString.DataBaseConnectionString = Configuration.GetConnectionString("DB_CONNECTION_STRING");

            using (var db = ServiceProvider.GetService<AppDbContext>())
            {
                if (db != null)
                {
                    db.Database.Migrate();

                }
            }
        }
    }
}