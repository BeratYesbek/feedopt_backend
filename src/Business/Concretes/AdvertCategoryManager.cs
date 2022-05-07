﻿using Business.Abstracts;
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
    /// <summary>
    /// This class working with dependency injection to manage our Advert Category using business logic
    /// Moreover this class includes aspect oriented programing design this is the reason. Each method 
    /// </summary>
    public class AdvertCategoryManager : IAdvertCategoryService
    {
        private readonly IAdvertCategoryDal _advertCategory; 

        public AdvertCategoryManager(IAdvertCategoryDal advertCategoryDal)
        {
            _advertCategory = advertCategoryDal;
        }


        /// <summary>
        /// This method run to add a new AdvertCategory, It is going to work with O(3)
        /// </summary>
        /// <param name="category"></param>
        /// <returns>It will return data result includes added AdvertCategory</returns>
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


        /// <summary>
        ///    This method run to update AdvertCategory. It is going to work with O(2)
        /// </summary>
        /// <param name="category">category</param>
        /// <returns>It will return success result</returns>
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

        /// <summary>
        ///  This method run to delete an AdvertCategory. It is going work with O(2)
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>It will return success result</returns>
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
        
        /// <summary>
        /// This method run to Get data by Advert Category Id, it is working with O(3)
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>It will return a data result includes Advert Category if it is exists</returns>
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
        
        /// <summary>
        /// This method run to GetAll data, it is going to work with O(3)
        /// </summary>
        /// <returns>It will return a data result includes list of advert category</returns>
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
