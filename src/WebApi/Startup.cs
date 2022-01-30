using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyrption;
using Core.Utilities.Security.JWT;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet.Actions;
using Core.Utilities.Language;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using WebApi.Config;
using WebApi.SignalR;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDistributedMemoryCache();


            services.AddDbContext<NervioDbContext>();

            services.AddScoped<IConfig, Config.Config>();

            services.AddSignalR();

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.IsEssential = true;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["Authorization"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            })); 
              



            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" }); });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new DefaultContractResolver();
            });

            services.AddControllersWithViews();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvcCore();

            //Globalization and Localization
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();


            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }
        //


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IConfig config)
        {
            // this method will be set our configuration
            config.Run();

            app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

            app.UseStaticFiles();

            app.UseSession();

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(Language.SupportedLanguage[0])
                .AddSupportedCultures(Language.SupportedLanguage)
                .AddSupportedUICultures(Language.SupportedLanguage);

            app.UseRequestLocalization(localizationOptions);



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notificationHub");
                endpoints.MapHub<NotificationHub>("/chatHub");

            });

        }
    }
}