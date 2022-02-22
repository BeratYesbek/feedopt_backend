using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.Cloud.Cloudinary;
using Core.Utilities.Mailer;
using DataAccess;
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
            DatabaseMigration();
            SetCloudinaryOptions();
            SetMailerOptions();
        }

        private void SetMailerOptions()
        {
            IConfigurationSection section = Configuration.GetSection("Mailer");
            Mailer.EmailOption = section["Email"];
            Mailer.PasswordOption = section["Password"];
            Mailer.HostOption = section["Host"];
            Mailer.PortOption = Convert.ToInt32(section["Port"]);
        }

        private void DatabaseMigration()
        {
            ConnectionString.DataBaseConnectionString = Configuration.GetConnectionString("DB_CONNECTION_STRING");

            using (var db = ServiceProvider.GetService<NervioDbContext>())
            {
                if (db != null)
                {
                   // db.Database.Migrate();

                }
            }
        }
    }
}