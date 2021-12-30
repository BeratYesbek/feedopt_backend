using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessAspect;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entity.concretes;

namespace Business.Abstracts
{
    public class AnimalSpeciesManager : IAnimalSpeciesService
    {
        private IAnimalSpeciesDal _animalSpeciesDal;

        public AnimalSpeciesManager(IAnimalSpeciesDal animalSpeciesDal)
        {
            _animalSpeciesDal = animalSpeciesDal;
        }

        //[SecuredOperation("AnimalSpecies.Add,Admin")]
        [ValidationAspect(typeof(AnimalSpeciesValidator))]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IResult Add(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Add(animalSpecies);
            return new SuccessResult();
        }

        [SecuredOperation("AnimalSpecies.Update,Admin")]
        [ValidationAspect(typeof(AnimalSpeciesValidator))]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IResult Update(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Update(animalSpecies);
            return new SuccessResult();
        }

        [SecuredOperation("AnimalSpecies.Delete,Admin")]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll")]
        [LogAspect(typeof(FileLogger))]
        public IResult Delete(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Delete(animalSpecies);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation("AnimalSpecies.Get,User")]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<AnimalSpecies> Get(int id)
        {
            var data = _animalSpeciesDal.Get(a => a.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<AnimalSpecies>(data);
            }

            return new ErrorDataResult<AnimalSpecies>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("AnimalSpecies.GetAll,User")]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<AnimalSpecies>> GetAll()
        {
            var data = _animalSpeciesDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AnimalSpecies>>(data);
            }

            return new ErrorDataResult<List<AnimalSpecies>>(null);
        }
    }
}