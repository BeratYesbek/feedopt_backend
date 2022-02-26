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
using System.Collections.Generic;

namespace Business.Concretes
{
    public class AdvertCategoryManager : IAdvertCategoryService
    {
        private readonly IAdvertCategoryDal _advertCategory; 

        public AdvertCategoryManager(IAdvertCategoryDal advertCategoryDal)
        {
            _advertCategory = advertCategoryDal;
        }

        [ValidationAspect(typeof(AdvertCategoryValidator))]
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdvertCategoryService.GetAll")]
        [CacheRemoveAspect("IAdvertCategoryService.Get")]
        [SecuredOperation($"{Role.AdvertCategoryAdd},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<AdvertCategory> Add(AdvertCategory category)
        {
            var data = _advertCategory.Add(category);
            if (data is not null)
            {
                
                return new SuccessDataResult<AdvertCategory>(data);
            }

            return new ErrorDataResult<AdvertCategory>(null);
        }

        [ValidationAspect(typeof(AdvertCategoryValidator))]
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdvertCategoryService.GetAll")]
        [CacheRemoveAspect("IAdvertCategoryService.Get")]
        [SecuredOperation($"{Role.AdvertCategoryUpdate},{Role.SuperAdmin},{Role.Admin}")]
        public IResult Update(AdvertCategory category)
        {
            _advertCategory.Update(category);
            return new SuccessResult();
        }

        [ValidationAspect(typeof(AdvertCategoryValidator))]
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdvertCategoryService.GetAll")]    
        [CacheRemoveAspect("IAdvertCategoryService.Get")]
        [SecuredOperation($"{Role.AdvertCategoryDelete},{Role.SuperAdmin},{Role.Admin}")]
        public IResult Delete(AdvertCategory category)
        {
            _advertCategory.Delete(category);
            return new SuccessResult();
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<AdvertCategory> Get(int id)
        {
            var data = _advertCategory.Get(a => a.Id == id);
            if (data is not null)
            {
                return new SuccessDataResult<AdvertCategory>(data);
            }

            return new ErrorDataResult<AdvertCategory>(null);
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
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
