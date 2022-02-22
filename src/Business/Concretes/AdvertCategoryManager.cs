using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    public class AdvertCategoryManager : IAdvertCategoryService
    {
        public IDataResult<AdvertCategory> Add(AdvertCategory category)
        {
            throw new NotImplementedException();
        }

        public IResult Update(AdvertCategory category)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(AdvertCategory category)
        {
            throw new NotImplementedException();
        }

        public IDataResult<AdvertCategory> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<AdvertCategory>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
