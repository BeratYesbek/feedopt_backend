using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Entity.concretes;
using System.Collections.Generic;
using Business.BusinessAspect.SecurityAspect;
using Business.Security.Role;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DataAccess.Abstracts;

using IResult = Core.Utilities.Result.Abstracts.IResult;

namespace Business.Concretes
{
    /// <summary>
    /// This class manage Animal Category. Whenever need to manage something on that, everything should do in this class because of SOLID - Single Responsibility Principle
    /// </summary>
    public class AnimalCategoryManager : IAnimalCategoryService
    {
        private readonly IAnimalCategoryDal _animalCategoryDal;

        public AnimalCategoryManager(IAnimalCategoryDal animalCategoryDal)
        {
            _animalCategoryDal = animalCategoryDal;
        }

        /// <summary>
        /// Animal Category is added by this method. It is going to work with O(3)
        /// </summary>
        /// <param name="animalCategory"></param>
        /// <returns>IResult</returns>
        [SecuredOperation($"{Role.AnimalCategoryAdd},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [ValidationAspect(typeof(AnimalCategoryValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [LogAspect(typeof(FileLogger), Priority = 5)]
        [CacheRemoveAspect("IAnimalCategoryService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IAnimalCategoryService.Get", Priority = 6)]
        [CacheRemoveAspect("IOptionService.GetOptions",Priority = 7)]
        public IDataResult<AnimalCategory> Add(AnimalCategory animalCategory)
        {
            var result = _animalCategoryDal.Add(animalCategory);
            if (result is not  null)
            {
                return new SuccessDataResult<AnimalCategory>(result);
            }
            return new SuccessDataResult<AnimalCategory>(null);
        }

        /// <summary>
        /// Animal Category is updated by this method. It is going to work with O(2)
        /// </summary>
        /// <param name="animalCategory"></param>
        /// <returns>IResult</returns>
        [SecuredOperation($"{Role.AnimalCategoryUpdate},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [ValidationAspect(typeof(AnimalCategoryValidator),Priority = 2)]
        [PerformanceAspect(5,Priority = 3)]
        [LogAspect(typeof(DatabaseLogger),Priority = 4)]
        [CacheRemoveAspect("IAnimalCategoryService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IAnimalCategoryService.Get", Priority = 6)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 7)]
        public IResult Update(AnimalCategory animalCategory)
        {
            _animalCategoryDal.Update(animalCategory);
            return new SuccessResult();
        }

        /// <summary>
        /// Animal Category is deleted by this method. It is going to work with O(2)
        /// </summary>
        /// <param name="animalCategory"></param>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.AnimalCategoryDelete},{Role.Admin},{Role.SuperAdmin}",Priority = 1)]
        [PerformanceAspect(5,Priority = 2)]
        [LogAspect(typeof(DatabaseLogger),Priority = 3)]
        [CacheRemoveAspect("IAnimalCategoryService.GetAll", Priority = 4)]
        [CacheRemoveAspect("IAnimalCategoryService.Get", Priority = 5)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 6)]
        public IResult Delete(AnimalCategory animalCategory)
        {
            _animalCategoryDal.Delete(animalCategory);
            return new SuccessResult();
        }

        /// <summary>
        /// This method get animal category by ID. It is going to work with (3)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.AnimalCategoryGet},{Role.User},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [PerformanceAspect(5,Priority = 2)]
        [CacheAspect(Priority = 3)]
        [LogAspect(typeof(DatabaseLogger),Priority = 4)]
        public IDataResult<AnimalCategory> Get(int id)
        {
            var data = _animalCategoryDal.Get(a => a.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<AnimalCategory>(data);
            }

            return new ErrorDataResult<AnimalCategory>(null);
        }

        /// <summary>
        /// This method get all animal categories
        /// </summary>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.AnimalCategoryGetAll},{Role.User},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [LogAspect(typeof(FileLogger), Priority = 4)]
        [CacheAspect(Priority = 5)]
        public IDataResult<List<AnimalCategory>> GetAll()
        {
            var data = _animalCategoryDal.GetAll();

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AnimalCategory>>(data);
            }

            return new ErrorDataResult<List<AnimalCategory>>(null);
        }
    }
}