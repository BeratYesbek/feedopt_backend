using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    public class AdvertCategoryManager : IAdvertCategoryService
    {
        private readonly IAdvertCategoryDal _advertCategory;

        public AdvertCategoryManager(IAdvertCategoryDal advertCategoryDal)
        {
            _advertCategory = advertCategoryDal;
        }

        public IDataResult<AdvertCategory> Add(AdvertCategory category)
        {
            var data = _advertCategory.Add(category);
            if (data is not null)
            {
                return new SuccessDataResult<AdvertCategory>(data);
            }

            return new ErrorDataResult<AdvertCategory>(null);
        }

        public IResult Update(AdvertCategory category)
        {
            _advertCategory.Update(category);
            return new SuccessResult();
        }

        public IResult Delete(AdvertCategory category)
        {
            _advertCategory.Delete(category);
            return new SuccessResult();
        }

        public IDataResult<AdvertCategory> Get(int id)
        {
            var data = _advertCategory.Get(a => a.Id == id);
            if (data is not null)
            {
                return new SuccessDataResult<AdvertCategory>(data);
            }

            return new ErrorDataResult<AdvertCategory>(null);
        }

        public IDataResult<List<AdvertCategory>> GetAll()
        {
            var data = _advertCategory.GetAll();
            if (data is not null)
            {
                return new SuccessDataResult<List<AdvertCategory>>(data);
            }

            return new ErrorDataResult<List<AdvertCategory>>(null);
        }
    }
}
