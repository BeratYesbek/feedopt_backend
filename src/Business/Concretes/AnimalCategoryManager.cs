using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Entity.concretes;
using System.Collections.Generic;
using Business.BusinessAspect;
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
    public class AnimalCategoryManager : IAnimalCategoryService
    {
        private readonly IAnimalCategoryDal _animalCategoryDal;

        public AnimalCategoryManager(IAnimalCategoryDal animalCategoryDal)
        {
            _animalCategoryDal = animalCategoryDal;
        }

        [SecuredOperation($"{Role.AnimalCategoryAdd},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [ValidationAspect(typeof(AnimalCategoryValidator))]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect("IAnimalCategoryService.GetAll")]
        [PerformanceAspect(5)]
        public IDataResult<AnimalCategory> Add(AnimalCategory animalCategory)
        {
            var result = _animalCategoryDal.Add(animalCategory);
            if (result is not  null)
            {
                return new SuccessDataResult<AnimalCategory>(result);
            }
            return new SuccessDataResult<AnimalCategory>(null);
        }

        [SecuredOperation($"{Role.AnimalCategoryUpdate},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [CacheRemoveAspect("IAnimalCategoryService.GetAll")]
        [ValidationAspect(typeof(AnimalCategoryValidator))]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Update(AnimalCategory animalCategory)
        {
            _animalCategoryDal.Update(animalCategory);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.AnimalCategoryDelete},{Role.Admin},{Role.SuperAdmin}",Priority = 1)]
        [CacheRemoveAspect("IAnimalCategoryService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(AnimalCategory animalCategory)
        {
            _animalCategoryDal.Delete(animalCategory);
            return new SuccessResult();
        }


        [SecuredOperation($"{Role.AnimalSpeciesGet},{Role.User},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [PerformanceAspect(5)]
        [CacheAspect]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<AnimalCategory> Get(int id)
        {
            var data = _animalCategoryDal.Get(a => a.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<AnimalCategory>(data);
            }

            return new ErrorDataResult<AnimalCategory>(null);
        }


        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.Admin},{Role.SuperAdmin}",Priority = 0)]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<AnimalCategory>> GetAll()
        {
            var data = _animalCategoryDal.GetAll(null,true);

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AnimalCategory>>(data);
            }

            return new ErrorDataResult<List<AnimalCategory>>(null);
        }
    }
}