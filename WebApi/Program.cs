using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolver.Autofac;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var service = (IServiceScopeFactory) host.Services.GetService(typeof(IServiceScopeFactory));
            using (var db = service.CreateScope().ServiceProvider.GetService<NervioDbContext>())
            {
                db.Database.Migrate();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacBusinessModule());
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
/*
 *
 * DependencyResolutionException: None of the constructors found with 'Autofac.Core.Activators.Reflection.DefaultConstructorFinder' on type 'Microsoft.AspNetCore.Session.DistributedSessionStore' can be invoked with the available services and parameters:
Cannot resolve parameter 'Microsoft.Extensions.Caching.Distributed.IDistributedCache cache' of constructor 'Void .ctor(Microsoft.Extensions.Caching.Distributed.IDistributedCache, Microsoft.Extensions.Logging.ILoggerFactory)'.
 */