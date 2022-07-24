using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
using Core.Utilities.Language;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json.Serialization;
using WebApi.Config;
using WebApi.SignalR;
using WebAPI.Config;
using Business.Concretes;
using Business.Abstracts;
using Amazon.Runtime;
using Amazon.Extensions.NETCore.Setup;
using Core.Utilities.Cloud.Aws;
using Amazon;
using Amazon.S3;
using NLog;

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

            services.AddMvc(options =>
            {
                options.AddCommaSeparatedArrayModelBinderProvider();
            });

            var awsConfiguration = new AWSServiceConfiguration();
            var awsSettingsSection = Configuration.GetSection("AWSS3Configuration");
            awsSettingsSection.Bind(awsConfiguration);

            var awsOptions = new AWSOptions
            {
                Credentials = new BasicAWSCredentials(awsConfiguration.AccessKey, awsConfiguration.SecretKey),
                Region = RegionEndpoint.GetBySystemName(awsConfiguration.Region)
            };

            services.AddAWSService<IAmazonS3>(awsOptions);
            services.Configure<AWSServiceConfiguration>(awsSettingsSection);


            services.AddSignalR();
            services.AddMvcCore();
            services.AddHangfireServer();
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IConfig, Config.Config>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHangfire(x => x.UsePostgreSqlStorage(Configuration.GetConnectionString("DB_CONNECTION_STRING")));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" }); });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver =new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });




            //Globalization and Localization
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
            });


        }



        public void Configure(IApplicationBuilder app, IConfig config)
        {
            config.Run();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseHangfireDashboard();
            app.UseRouting();
            app.UseAuthorization();
            app.VerifyUserRequest();
            app.UseSwagger();
            
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));


            //app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCors(builder => builder.WithOrigins("http://localhost:3001/", "http://localhost:3000/", "http://127.0.0.1:3000/").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(Language.SupportedLanguage[0])
                .AddSupportedCultures(Language.SupportedLanguage)
                .AddSupportedUICultures(Language.SupportedLanguage);
            app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub", map => { });
            });



        }
    }
}