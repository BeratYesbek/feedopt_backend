using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;
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
