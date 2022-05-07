using System.Collections.Generic;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Security.Role;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;
using IResult = Core.Utilities.Result.Abstracts.IResult;


namespace Business.Concretes
{
    public class AdvertImageManager : IAdvertImageService
    {
        private readonly IAdvertImageDal _advertImageDal;

        public AdvertImageManager(IAdvertImageDal advertImageDal)
        {
            _advertImageDal = advertImageDal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdvertImageService.GetByAdvertId")]
        [CacheRemoveAspect("IAdvertImageService.Get")]
        [CacheRemoveAspect("IAdvertImageService.GetAll")]
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        [ValidationAspect(typeof(AdvertImageValidator))]
        public IResult Add(AdvertImage image)
        {
            _advertImageDal.Add(image);

            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect("IAdvertImageService.GetByAdvertId")]
        [CacheRemoveAspect("IAdvertImageService.Get")]
        [CacheRemoveAspect("IAdvertImageService.GetAll")]
        [SecuredOperation($"{Role.AdvertImageUpdate},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IResult Delete(AdvertImage image)
        {
            _advertImageDal.Delete(image);
            return new SuccessResult();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation($"{Role.AdvertCategoryGet},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<AdvertImage> Get(int id)
        {
            var data = _advertImageDal.Get(a => a.Id == id);

            if (data is not null)
            {
                return new SuccessDataResult<AdvertImage>(data);
            }

            return new ErrorDataResult<AdvertImage>(null);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<List<AdvertImage>> GetAll()
        {
            var data = _advertImageDal.GetAll();

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdvertImage>>(data);
            }

            return new ErrorDataResult<List<AdvertImage>>(null);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation($"{Role.AdvertCategoryGet},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<List<AdvertImage>> GetByAdvertId(int id)
        {
            var data = _advertImageDal.GetAll(a => a.AdvertId == id);

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdvertImage>>(data);
            }

            return new ErrorDataResult<List<AdvertImage>>(null);
        }

        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect("IAdvertImageService.GetByAdvertId")]
        [CacheRemoveAspect("IAdvertImageService.Get")]
        [CacheRemoveAspect("IAdvertImageService.GetAll")]
        [SecuredOperation($"{Role.AdvertUpdate},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        [ValidationAspect(typeof(AdvertImageValidator))]

        public IResult Update(AdvertImage image)
        {
            _advertImageDal.Update(image);
            return new SuccessResult();
        }
    }
}