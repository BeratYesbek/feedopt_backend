using System.Collections.Generic;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Messages.MethodMessages;
using Business.Security.Role;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;
using Hangfire;

namespace Business.Concretes
{
    public class FavoriteAdvertManager : IFavoriteAdvertService
    {
        private readonly IFavoriteAdvertDal _favoriteAdvertDal;

        public FavoriteAdvertManager(IFavoriteAdvertDal favoriteAdvertDal)
        {
            _favoriteAdvertDal = favoriteAdvertDal;
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.ChatAdd}")]
        [ValidationAspect(typeof(FavoriteAdvertValidator))]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAll")]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAllDetailByUserId")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailByUserId")]
        [CacheRemoveAspect("IAdvertService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<FavoriteAdvert> Add(FavoriteAdvert favorite)
        {
            var data = _favoriteAdvertDal.Add(favorite);
            if (data != null)
            {
                return new SuccessDataResult<FavoriteAdvert>(data, FavoriteAdvertMessages.FavoriteRecordSuccess);
            }

            return new ErrorDataResult<FavoriteAdvert>(null, FavoriteAdvertMessages.FavoriteRecordFail);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertUpdate}")]
        [ValidationAspect(typeof(FavoriteAdvertValidator))]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAll")]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAllDetailByUserId")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailByUserId")]
        [CacheRemoveAspect("IAdvertService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Update(FavoriteAdvert favorite)
        {
            _favoriteAdvertDal.Update(favorite);
            return new SuccessResult(FavoriteAdvertMessages.FavoriteUpdateSuccess);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertDelete}")]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAll")]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAllDetailByUserId")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailByUserId")]
        [CacheRemoveAspect("IAdvertService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(FavoriteAdvert favorite)
        {
            _favoriteAdvertDal.Delete(favorite);
            return new SuccessResult(FavoriteAdvertMessages.FavoriteRemoveSuccess);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertGet}")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<FavoriteAdvert> Get(int id)
        {
            var data = _favoriteAdvertDal.Get(f => f.Id == id);
            if (data is not null)
            {
                return new SuccessDataResult<FavoriteAdvert>(data, FavoriteAdvertMessages.FavoriteGetSuccess);
            }
            return new ErrorDataResult<FavoriteAdvert>(null, FavoriteAdvertMessages.FavoriteGetFail);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertGetAll}")]
        //[CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<FavoriteAdvert>> GetAll()
        {
            var data = _favoriteAdvertDal.GetAll();
            if (data is not null)
            {
                return new SuccessDataResult<List<FavoriteAdvert>>(data, FavoriteAdvertMessages.FavoriteGetAllSuccess);
            }
            return new ErrorDataResult<List<FavoriteAdvert>>(null, FavoriteAdvertMessages.FavoriteGetAllFail);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertGetAll}")]
        //[CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<FavoriteAdvertReadDto>> GetAllDetailByUserId(int userId)
        {
            var latitude = CurrentUser.Latitude;
            var longitude = CurrentUser.Longitude;
            var data = _favoriteAdvertDal.GetAllDetailByFilter(f => f.UserId == userId, latitude, longitude);
            if (data is not null)
            {
                return new SuccessDataResult<List<FavoriteAdvertReadDto>>(data, FavoriteAdvertMessages.FavoriteGetAllSuccess);
            }
            return new ErrorDataResult<List<FavoriteAdvertReadDto>>(null, FavoriteAdvertMessages.FavoriteGetAllFail);
        }
    }
}