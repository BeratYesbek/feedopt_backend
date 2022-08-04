using System.Collections.Generic;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;
using IResult = Core.Utilities.Result.Abstracts.IResult;

namespace Business.Abstracts
{
    public interface IAdvertImageService
    {
        IResult Add(AdvertImage image);

        IResult Update(AdvertImage image);

        IResult Delete(AdvertImage image);

        IDataResult<List<AdvertImage>> GetByAdvertId(int id);

        IDataResult<AdvertImage> Get(int id);

        IDataResult<List<AdvertImage>> GetAll();

    }
}