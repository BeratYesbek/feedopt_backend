using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstracts;
using Business.BackgroundJob.Hangfire;
using Business.Concretes;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Castle.DynamicProxy;
using Core.Utilities.Cloud.Cloudinary;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entity.Concretes;
using Microsoft.AspNetCore.Session;

namespace Business.DependencyResolver.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();
            builder.RegisterType<EfColorDal>().As<IColorDal>().SingleInstance();

            builder.RegisterType<AgeRangeManager>().As<IAgeRangeService>().SingleInstance();
            builder.RegisterType<EfAgeRangeDal>().As<IAgeRangeDal>().SingleInstance();

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

            builder.RegisterType<TelegramManager>().As<ITelegramService>().SingleInstance();
            builder.RegisterType<SmsManager>().As<ISmsService>().SingleInstance();
            
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