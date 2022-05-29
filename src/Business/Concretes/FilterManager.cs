using Business.Abstracts;
using Business.BusinessAspect;
using Business.Security.Role;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class FilterManager : IFilterService
    {
        private readonly IFilterDal _filterDal;
        public FilterManager(IFilterDal filterDal)
        {
            _filterDal = filterDal;
        }

        
        public IDataResult<Filter> Add(Filter filter)
        {
            var data = _filterDal.Add(filter);
            if (data != null)
                return new SuccessDataResult<Filter>(data);
            return new ErrorDataResult<Filter>(null);
        }

        public IResult Delete(Filter filter)
        {
            _filterDal.Delete(filter);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        public IDataResult<List<Filter>> GetAll()
        {
            return new SuccessDataResult<List<Filter>>(_filterDal.GetAll(null,true));
        }

        public IDataResult<List<FilterDto>> GetByFilterType(string type)
        {
            return new SuccessDataResult<List<FilterDto>>(_filterDal.GetByFilterType(c => c.FilterType == type));
        }

        public IDataResult<Filter> GetById(int id)
        {
            return new SuccessDataResult<Filter>(_filterDal.Get(t => t.Id == id));
        }

        public IResult Update(Filter filter)
        {
            _filterDal.Update(filter);
            return new SuccessResult(""); 
        }
    }
}
