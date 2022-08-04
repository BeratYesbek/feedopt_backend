using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolver.Autofac;
using NLog.Web;
using NLog;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacBusinessModule());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var port = Environment.GetEnvironmentVariable("PORT");

                    webBuilder.UseStartup<Startup>()
                        .UseUrls("http://*:" + port);
                }).ConfigureLogging(opt =>
                {
                    opt.ClearProviders();
                    opt.SetMinimumLevel(LogLevel.Trace);
                }).UseNLog();
    }
}
