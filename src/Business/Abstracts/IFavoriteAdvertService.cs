using System.Collections.Generic;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;
using Entity.Dtos;

namespace Business.Abstracts
{
    public interface IFavoriteAdvertService
    {
        IDataResult<FavoriteAdvert> Add(FavoriteAdvert favorite);
        IResult Update(FavoriteAdvert favorite);
        IResult Delete(FavoriteAdvert favorite);
        IDataResult<List<FavoriteAdvertReadDto>> GetAllDetailByUserId(int userId);
    }
}