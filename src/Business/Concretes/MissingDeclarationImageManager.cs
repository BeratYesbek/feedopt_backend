using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities;
using Core.Utilities.Cloud.Cloudinary;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entity;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Business.Concretes
{
    public class MissingDeclarationImageManager : IMissingDeclarationImageService
    {
        private readonly IMissingDeclarationImageDal _missingDeclarationImageDal;
        private readonly ICloudinaryService _cloudinaryService;

        public MissingDeclarationImageManager(IMissingDeclarationImageDal missingDeclarationImageDal,
            ICloudinaryService cloudinaryService)
        {
            _missingDeclarationImageDal = missingDeclarationImageDal;
            _cloudinaryService = cloudinaryService;
        }

        [CacheRemoveAspect("IMissingDeclarationImageService.GetAll")]
        //  [SecuredOperation("MissingDeclarationImage.Add,User")]
        [PerformanceAspect(5)] //[LogAspect(typeof(FileLogger))]
        public IResult Add(MissingDeclarationImage missingDeclarationImage)
        {
            _missingDeclarationImageDal.Add(missingDeclarationImage);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IMissingDeclarationImageService.GetAll")]
      //  [SecuredOperation("MissingDeclarationImage.Update,User")]
        [PerformanceAspect(5)]
       // [LogAspect(typeof(FileLogger))]
        public IResult Update(MissingDeclarationImage missingDeclarationImage)
        {
            _missingDeclarationImageDal.Update(missingDeclarationImage);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IMissingDeclarationImageService.GetAll")]
      //  [SecuredOperation("MissingDeclarationImage.Delete,User")]
        [PerformanceAspect(5)]
       // [LogAspect(typeof(FileLogger))]
        public IResult Delete(MissingDeclarationImage missingDeclarationImage)
        {
            _missingDeclarationImageDal.Delete(missingDeclarationImage);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        //[SecuredOperation("MissingDeclarationImage.Get,User")]
       // [LogAspect(typeof(FileLogger))]
       [CacheAspect]
        public IDataResult<List<MissingDeclarationImage>> GetByMissingDeclarationId(int id)
        {
            var data = _missingDeclarationImageDal.GetAll(m => m.MissingDeclarationId == id);
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<MissingDeclarationImage>>(data);
            }

            return new ErrorDataResult<List<MissingDeclarationImage>>(null);
        }

        [PerformanceAspect(5)]
      //  [SecuredOperation("MissingDeclarationImage.Get,User")]
     //   [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        public IDataResult<MissingDeclarationImage> Get(int id)
        {
            var data = _missingDeclarationImageDal.Get(m => m.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<MissingDeclarationImage>(data);
            }

            return new ErrorDataResult<MissingDeclarationImage>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        //[SecuredOperation("MissingDeclarationImage.GetAll,User")]
        //[LogAspect(typeof(FileLogger))]
        public IDataResult<List<MissingDeclarationImage>> GetAll()
        {
            var data = _missingDeclarationImageDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<MissingDeclarationImage>>(data);
            }

            return new ErrorDataResult<List<MissingDeclarationImage>>(null);
        }
    }
}