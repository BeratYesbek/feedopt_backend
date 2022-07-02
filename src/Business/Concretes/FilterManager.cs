using Business.Abstracts;
using Business.BusinessAspect;
using Business.Security.Role;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
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
    /// <summary>
    /// This method manage filters. Whenever need to manage filters should do in this class because of SOLID - S => single responsibility principle
    /// </summary>
    public class FilterManager : IFilterService
    {
        private readonly IFilterDal _filterDal;
        public FilterManager(IFilterDal filterDal)
        {
            _filterDal = filterDal;
        }

        /// <summary>
        ///  Filters are managed by Admin and Super Admin in this method. Whenever wants to add new filter to a model use this method
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.FilterAdd},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [ValidationAspect(typeof(FilterValidator),Priority = 2)]
        [LogAspect(typeof(DatabaseLogger),Priority = 3)]
        [PerformanceAspect(5,Priority = 4)]
        [CacheRemoveAspect("IFilterService.GetAll",Priority = 5)]
        [CacheRemoveAspect("IFilterService.GetByFilterType",Priority = 6)]
        [CacheRemoveAspect("IFilterService.GetById", Priority = 7)]
        public IDataResult<Filter> Add(Filter filter)
        {
            var data = _filterDal.Add(filter);
            if (data != null)
                return new SuccessDataResult<Filter>(data);
            return new ErrorDataResult<Filter>(null);
        }

        /// <summary>
        /// Filters are managed by Admin and Super Admin in this method. Whenever wants to delete use this method
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>IResult</returns>
        [SecuredOperation($"{Role.FilterDelete},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [CacheRemoveAspect("IFilterService.GetAll", Priority = 4)]
        [CacheRemoveAspect("IFilterService.GetByFilterType", Priority = 5)]
        [CacheRemoveAspect("IFilterService.GetById", Priority = 6)]
        public IResult Delete(Filter filter)
        {
            _filterDal.Delete(filter);
            return new SuccessResult();
        }

        /// <summary>
        /// This method get all filters
        /// </summary>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.FilterGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(FilterValidator), Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [PerformanceAspect(5, Priority = 4)]
        [CacheAspect(Priority =5)]
        public IDataResult<List<Filter>> GetAll()
        {
            return new SuccessDataResult<List<Filter>>(_filterDal.GetAll(null,true));
        }

        /// <summary>
        /// This method get filters by filter type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.FilterGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(FilterValidator), Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [PerformanceAspect(5, Priority = 4)]
        [CacheAspect(Priority = 5)]
        public IDataResult<List<FilterDto>> GetByFilterType(string type)
        {
            return new SuccessDataResult<List<FilterDto>>(_filterDal.GetByFilterType(c => c.FilterType == type));
        }

        /// <summary>
        /// This method get single filter by filter ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.FilterGet},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(FilterValidator), Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [PerformanceAspect(5, Priority = 4)]
        [CacheAspect(Priority = 5)]
        public IDataResult<Filter> GetById(int id)
        {
            return new SuccessDataResult<Filter>(_filterDal.Get(t => t.Id == id));
        }

        /// <summary>
        ///  Filters are managed by Admin and Super Admin in this method. Whenever wants to update a filter use this method
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>IResult</returns>
        [SecuredOperation($"{Role.FilterUpdate},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [ValidationAspect(typeof(FilterValidator), Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [PerformanceAspect(5, Priority = 4)]
        [CacheRemoveAspect("IFilterService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IFilterService.GetByFilterType", Priority = 6)]
        [CacheRemoveAspect("IFilterService.GetById", Priority = 7)]
        public IResult Update(Filter filter)
        {
            _filterDal.Update(filter);
            return new SuccessResult(""); 
        }
    }
}
