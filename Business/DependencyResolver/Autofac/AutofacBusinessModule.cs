﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstracts;
using Business.Concretes;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entity.concretes;

namespace Business.DependencyResolver.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AnimalCategoryManager>().As<IAnimalCategoryService>().SingleInstance();
            builder.RegisterType<EfAnimalCategoryDal>().As<IAnimalCategoryDal>().SingleInstance();

            builder.RegisterType<AnimalSpeciesManager>().As<IAnimalSpeciesService>().SingleInstance();
            builder.RegisterType<EfAnimalSpeciesDal>().As<IAnimalSpeciesDal>().SingleInstance();

            builder.RegisterType<AdoptionNoticeManager>().As<IAdoptionNoticeService>().SingleInstance();
            builder.RegisterType<EfAdoptionNoticeDal>().As<IAdoptionNoticeDal>().SingleInstance();

            builder.RegisterType<MissingDeclarationManager>().As<IMissingDeclarationService>().SingleInstance();
            builder.RegisterType<EfMissingDeclarationDal>().As<IMissingDeclarationDal>().SingleInstance();

            builder.RegisterType<MissingDeclarationImageManager>().As<IMissingDeclarationImageService>()
                .SingleInstance();
            builder.RegisterType<EfMissingDeclarationImageDal>().As<IMissingDeclarationImageDal>().SingleInstance();

            builder.RegisterType<AdoptionNoticeImageManager>().As<IAdoptionNoticeImageService>().SingleInstance();
            builder.RegisterType<EfAdoptionNoticeImageDal>().As<IAdoptionNoticeImageDal>().SingleInstance();

            builder.RegisterType<LocationManager>().As<ILocationService>().SingleInstance();
            builder.RegisterType<EfLocationDal>().As<ILocationDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}