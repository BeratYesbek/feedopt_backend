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
    /// <summary>
    /// This class is working with dependency injection to manage Advert Images of an Advert using business logic
    /// Moreover, this class includes aspect oriented programming design, Therefore Each of the methods is going to process something before the runtime or after the runtime
    /// </summary>
    public class AdvertImageManager : IAdvertImageService
    {
        private readonly IAdvertImageDal _advertImageDal;

        public AdvertImageManager(IAdvertImageDal advertImageDal)
        {
            _advertImageDal = advertImageDal;
        }

        /// <summary>
        /// This method run to add a new AdvertImage, It is going to work with O(2)
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>It will return result</returns>
       // [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(AdvertImageValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IAdvertImageService.GetByAdvertId", Priority = 5)]
        [CacheRemoveAspect("IAdvertImageService.Get", Priority = 6)]
        [CacheRemoveAspect("IAdvertImageService.GetAll", Priority = 7)]
        public IResult Add(AdvertImage image)
        {
            _advertImageDal.Add(image);
            return new SuccessResult();
        }

        /// <summary>
        /// This method run to delete an AdvertImage, It is going to work with O(2)
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>It will return result</returns>
       // [SecuredOperation($"{Role.AdvertImageDelete},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheRemoveAspect("IAdvertImageService.GetByAdvertId", Priority = 4)]
        [CacheRemoveAspect("IAdvertImageService.Get", Priority = 5)]
        [CacheRemoveAspect("IAdvertImageService.GetAll", Priority = 6)]
        public IResult Delete(AdvertImage image)
        {
            _advertImageDal.Delete(image);
            return new SuccessResult();
        }

        /// <summary>
        /// This method run to get single AdvertImage, It is going to work with O(4) without Linq expression
        /// </summary>
        /// <param name="id"></param>
        /// <returns>It will return data result that includes single data</returns>
       // [SecuredOperation($"{Role.AdvertImageGet},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<AdvertImage> Get(int id)
        {
            var data = _advertImageDal.Get(a => a.Id == id);

            if (data is not null)
            {
                return new SuccessDataResult<AdvertImage>(data);
            }

            return new ErrorDataResult<AdvertImage>(null);
        }

        /// <summary>
        /// This method run to get all AdvertImages, It is going to work with O(4) without Linq expression
        /// </summary>
        /// <returns>It will return data result that includes Advert Images</returns>
       // [SecuredOperation($"{Role.AdvertImageGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<List<AdvertImage>> GetAll()
        {
            var data = _advertImageDal.GetAll();

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdvertImage>>(data);
            }

            return new ErrorDataResult<List<AdvertImage>>(null);
        }


        /// <summary>
        /// This method to run get Advert Image by Advert ID, It is going to work with O(4) without Linq expression
        /// </summary>
        /// <param name="id">advertId</param>
        /// <returns>It will return data result that includes list of Advert Images</returns>
      //  [SecuredOperation($"{Role.AdvertImageGet},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<List<AdvertImage>> GetByAdvertId(int id)
        {
            var data = _advertImageDal.GetAll(a => a.AdvertId == id && !a.IsDeleted);

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdvertImage>>(data);
            }

            return new ErrorDataResult<List<AdvertImage>>(null);
        }


        /// <summary>
        /// This method to run update an Advert Image
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>It will return result</returns>
      //  [SecuredOperation($"{Role.AdvertUpdate},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(AdvertImageValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IAdvertImageService.GetByAdvertId", Priority = 5)]
        [CacheRemoveAspect("IAdvertImageService.Get", Priority = 6)]
        [CacheRemoveAspect("IAdvertImageService.GetAll", Priority = 7)]
        public IResult Update(AdvertImage image)
        {
            _advertImageDal.Update(image);
            return new SuccessResult();
        }
    }
}