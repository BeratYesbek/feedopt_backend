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

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertAdd}",Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [ValidationAspect(typeof(FavoriteAdvertValidator),Priority = 4)]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAll",Priority = 5)]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAllDetailByUserId",Priority =6)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail",Priority = 7)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById",Priority = 8)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter",Priority = 9)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailByUserId",Priority = 10)]
        [CacheRemoveAspect("IAdvertService.GetAll",Priority = 11)]
        public IDataResult<FavoriteAdvert> Add(FavoriteAdvert favorite)
        {
            var data = _favoriteAdvertDal.Add(favorite);
            if (data != null)
            {
                return new SuccessDataResult<FavoriteAdvert>(data, FavoriteAdvertMessages.FavoriteRecordSuccess);
            }

            return new ErrorDataResult<FavoriteAdvert>(null, FavoriteAdvertMessages.FavoriteRecordFail);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertUpdate}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [ValidationAspect(typeof(FavoriteAdvertValidator), Priority = 4)]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAllDetailByUserId", Priority = 6)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail", Priority = 7)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById", Priority = 8)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter", Priority = 9)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailByUserId", Priority = 10)]
        [CacheRemoveAspect("IAdvertService.GetAll", Priority = 11)]
        public IResult Update(FavoriteAdvert favorite)
        {
            _favoriteAdvertDal.Update(favorite);
            return new SuccessResult(FavoriteAdvertMessages.FavoriteUpdateSuccess);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertDelete}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [ValidationAspect(typeof(FavoriteAdvertValidator), Priority = 4)]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IFavoriteAdvertService.GetAllDetailByUserId", Priority = 6)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail", Priority = 7)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById", Priority = 8)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter", Priority = 9)]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailByUserId", Priority = 10)]
        [CacheRemoveAspect("IAdvertService.GetAll", Priority = 11)]
        public IResult Delete(FavoriteAdvert favorite)
        {
            _favoriteAdvertDal.Delete(favorite);
            return new SuccessResult(FavoriteAdvertMessages.FavoriteRemoveSuccess);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertGet}",Priority =1)]
        [PerformanceAspect(5,Priority =2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        public IDataResult<FavoriteAdvert> Get(int id)
        {
            var data = _favoriteAdvertDal.Get(f => f.Id == id);
            if (data is not null)
            {
                return new SuccessDataResult<FavoriteAdvert>(data, FavoriteAdvertMessages.FavoriteGetSuccess);
            }
            return new ErrorDataResult<FavoriteAdvert>(null, FavoriteAdvertMessages.FavoriteGetFail);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertGetAll}",Priority = 1)]
        [CacheAspect(Priority = 2)]
        [PerformanceAspect(5,Priority = 3)]
        [LogAspect(typeof(DatabaseLogger),Priority = 4)]
        public IDataResult<List<FavoriteAdvert>> GetAll()
        {
            var data = _favoriteAdvertDal.GetAll();
            if (data is not null)
            {
                return new SuccessDataResult<List<FavoriteAdvert>>(data, FavoriteAdvertMessages.FavoriteGetAllSuccess);
            }
            return new ErrorDataResult<List<FavoriteAdvert>>(null, FavoriteAdvertMessages.FavoriteGetAllFail);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertGetAll}",Priority = 1)]
        [CacheAspect(Priority = 2)]
        [PerformanceAspect(5,Priority = 3)]
        [LogAspect(typeof(DatabaseLogger),Priority = 4)]
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