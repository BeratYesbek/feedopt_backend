using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstracts;
using Business.Concretes;
using Castle.DynamicProxy;
using Core.Utilities.Cloud.Cloudinary;
using Core.Utilities.FileHelper;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entity.concretes;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;

namespace Business.DependencyResolver.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FavoriteAdvertManager>().As<IFavoriteAdvertService>().SingleInstance();
            builder.RegisterType<EfFavoriteAdvertDal>().As<IFavoriteAdvertDal>().SingleInstance();
            
            builder.RegisterType<AdvertCategoryManager>().As<IAdvertCategoryService>().SingleInstance();
            builder.RegisterType<EfAdvertCategoryDal>().As<IAdvertCategoryDal>().SingleInstance();

            builder.RegisterType<AdvertManager>().As<IAdvertService>().SingleInstance();
            builder.RegisterType<EfAdvertDal>().As<IAdvertDal>().SingleInstance();

            builder.RegisterType<AnimalCategoryManager>().As<IAnimalCategoryService>().SingleInstance();
            builder.RegisterType<EfAnimalCategoryDal>().As<IAnimalCategoryDal>().SingleInstance();

            builder.RegisterType<AnimalSpeciesManager>().As<IAnimalSpeciesService>().SingleInstance();
            builder.RegisterType<EfAnimalSpeciesDal>().As<IAnimalSpeciesDal>().SingleInstance();

            builder.RegisterType<AdvertImageManager>().As<IAdvertImageService>().SingleInstance();
            builder.RegisterType<EfAdvertImageDal>().As<IAdvertImageDal>().SingleInstance();

            builder.RegisterType<LocationManager>().As<ILocationService>().SingleInstance();
            builder.RegisterType<EfLocationDal>().As<ILocationDal>().SingleInstance();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<ChatManager>().As<IChatService>().SingleInstance();
            builder.RegisterType<EfChatDal>().As<IChatDal>().SingleInstance();


            builder.RegisterType<SupportManager>().As<ISupportService>().SingleInstance();
            builder.RegisterType<EfTicketDal>().As<ITicketDal>().SingleInstance();

            builder.RegisterType<SupportFileManager>().As<ISupportFileService>().SingleInstance();
            builder.RegisterType<EfTicketFileDal>().As<ITicketFileDal>().SingleInstance();

            builder.RegisterType<CloudinaryService>().As<ICloudinaryService>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            builder.RegisterType<DistributedSessionStore>().As<ISessionStore>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}