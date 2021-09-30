using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Concretes;
using Entity.concretes;
using System;
using System.Collections.Generic;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstracts;

namespace Business.Concretes
{
    public class AnimalCategoryManager : IAnimalCategoryService
    {

        private readonly IAnimalCategoryDal _animalCategoryDal;

        public AnimalCategoryManager(IAnimalCategoryDal animalCategoryDal)
        {
            _animalCategoryDal = animalCategoryDal;
        }
        
        [ValidationAspect(typeof(AnimalCategoryValidator))]
        [CacheRemoveAspect("IAnimalCategoryService.GetAll")]
        [PerformanceAspect(5)]
        public IDataResult<AnimalCategory> Add(AnimalCategory animalCategory)
        {
            var result = _animalCategoryDal.Add(animalCategory);
            return new SuccessDataResult<AnimalCategory>(result);
        }

        [CacheRemoveAspect("IAnimalCategoryService.GetAll")]
        [ValidationAspect(typeof(AnimalCategoryValidator))]
        [PerformanceAspect(5)]
        public IResult Update(AnimalCategory animalCategory)
        {
            _animalCategoryDal.Update(animalCategory);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IAnimalCategoryService.GetAll")]
        [PerformanceAspect(5)]
        public IResult Delete(AnimalCategory animalCategory)
        {
            _animalCategoryDal.Delete(animalCategory);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        public IDataResult<AnimalCategory> Get(int id)
        {
            var data = _animalCategoryDal.Get(a => a.AnimalCategoryId == id);
            if (data != null)
            {
                return new SuccessDataResult<AnimalCategory>(data);
            }

            return new ErrorDataResult<AnimalCategory>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
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