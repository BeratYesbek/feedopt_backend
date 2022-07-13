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

namespace Business.Concretes
{
    /// <summary>
    /// This method manage favorite adverts. Whenever need to manage favorites should do in this class because of SOLID - S => single responsibility principle
    /// </summary>
    public class FavoriteAdvertManager : IFavoriteAdvertService
    {
        private readonly IFavoriteAdvertDal _favoriteAdvertDal;

        public FavoriteAdvertManager(IFavoriteAdvertDal favoriteAdvertDal)
        {
            _favoriteAdvertDal = favoriteAdvertDal;
        }

        /// <summary>
        ///  Whenever user wants to add new favorite advert use this method.
        /// </summary>
        /// <param name="favorite"></param>
        /// <returns>IDataResult</returns>
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

        /// <summary>
        /// Whenever wants to update already added favorite item use this method
        /// </summary>
        /// <param name="favorite"></param>
        /// <returns>IResult</returns>
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

        /// <summary>
        /// Whenever wants to delete already added favorite item use this method
        /// </summary>
        /// <param name="favorite"></param>
        /// <returns>IResult</returns>
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

        /// <summary>
        /// This method get all favorite item by user ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.FavoriteAdvertGetAll}",Priority = 1)]
        [CacheAspect(Priority = 2)]
        [PerformanceAspect(5,Priority = 3)]
        [LogAspect(typeof(DatabaseLogger),Priority = 4)]
        public IDataResult<List<FavoriteAdvertReadDto>> GetAllDetailByUserId(int userId)
        {
            var latitude = CurrentUser.Latitude;
            var longitude = CurrentUser.Longitude;
            var data = _favoriteAdvertDal.GetAllDetailByFilter(f => f.UserId == CurrentUser.User.Id, CurrentUser.User.Id,latitude, longitude);
            if (data is not null)
            {
                return new SuccessDataResult<List<FavoriteAdvertReadDto>>(data, FavoriteAdvertMessages.FavoriteGetAllSuccess);
            }
            return new ErrorDataResult<List<FavoriteAdvertReadDto>>(null, FavoriteAdvertMessages.FavoriteGetAllFail);
        }
    }
}