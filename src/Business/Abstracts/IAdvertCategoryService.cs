using System.Collections.Generic;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface IAdvertCategoryService
    {
        IDataResult<AdvertCategory> Add(AdvertCategory category);

        IResult Update(AdvertCategory category);

        IResult Delete(AdvertCategory category);

        IDataResult<AdvertCategory> Get(int id);

        IDataResult<List<AdvertCategory>> GetAll();
    }
}
