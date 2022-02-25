using System.Collections.Generic;
using Business.Abstracts;
using Business.Messages.MethodMessages;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;

namespace Business.Concretes
{
    public class FavoriteAdvertManager : IFavoriteAdvertService
    {
        private readonly IFavoriteAdvertDal _favoriteAdvertDal;

        public FavoriteAdvertManager(IFavoriteAdvertDal favoriteAdvertDal)
        {
            _favoriteAdvertDal = favoriteAdvertDal;
        }

        public IDataResult<FavoriteAdvert> Add(FavoriteAdvert favorite)
        {
            var data = _favoriteAdvertDal.Add(favorite);
            if (data != null)
            {
                return new SuccessDataResult<FavoriteAdvert>(data,FavoriteAdvertMessages.FavoriteRecordSuccess);
            }

            return new ErrorDataResult<FavoriteAdvert>(null, FavoriteAdvertMessages.FavoriteRecordFail);
        }

        public IResult Update(FavoriteAdvert favorite)
        {
            _favoriteAdvertDal.Update(favorite);
            return new SuccessResult(FavoriteAdvertMessages.FavoriteUpdateSuccess);
            return new ErrorResult();
        }

        public IResult Delete(FavoriteAdvert favorite)
        {
            _favoriteAdvertDal.Delete(favorite);
            return new SuccessResult(FavoriteAdvertMessages.FavoriteRemoveSuccess);
        }

        public IDataResult<FavoriteAdvert> Get(int id)
        {
            var data = _favoriteAdvertDal.Get(f => f.Id == id);
            if (data is not null)
            {
                return new SuccessDataResult<FavoriteAdvert>(data, FavoriteAdvertMessages.FavoriteGetSuccess);
            }
            return new ErrorDataResult<FavoriteAdvert>(null, FavoriteAdvertMessages.FavoriteGetFail);
        }

        public IDataResult<List<FavoriteAdvert>> GetAll()
        {
            var data = _favoriteAdvertDal.GetAll();
            if (data is not null)
            {
                return new SuccessDataResult<List<FavoriteAdvert>>(data, FavoriteAdvertMessages.FavoriteGetAllSuccess);
            }
            return new ErrorDataResult<List<FavoriteAdvert>>(null, FavoriteAdvertMessages.FavoriteGetAllFail);
        }

        public IDataResult<List<FavoriteAdvertReadDto>> GetAllDetailByUserId(int userId)
        {
            var data = _favoriteAdvertDal.GetAllDetailByFilter(f => f.UserId == userId);
            if (data is not null)
            {
                return new SuccessDataResult<List<FavoriteAdvertReadDto>>(data, FavoriteAdvertMessages.FavoriteGetAllSuccess);
            }
            return new ErrorDataResult<List<FavoriteAdvertReadDto>>(null, FavoriteAdvertMessages.FavoriteGetAllFail);
        }
    }
}