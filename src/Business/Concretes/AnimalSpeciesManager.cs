using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [SecuredOperation($"{Role.AnimalCategoryAdd},{Role.Admin},{Role.SuperAdmin}")]
        [ValidationAspect(typeof(AnimalSpeciesValidator))]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Add(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Add(animalSpecies);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.AnimalSpeciesUpdate},{Role.Admin},{Role.SuperAdmin}")]
        [ValidationAspect(typeof(AnimalSpeciesValidator))]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Update(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Update(animalSpecies);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.AnimalSpeciesDelete},{Role.Admin},{Role.SuperAdmin}")]
        [ValidationAspect(typeof(AnimalSpeciesValidator))]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Delete(animalSpecies);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation($"{Role.AnimalSpeciesGet},{Role.Admin},{Role.User},{Role.SuperAdmin}")]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<AnimalSpecies> Get(int id)
        {
            var data = _animalSpeciesDal.Get(a => a.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<AnimalSpecies>(data);
            }

            return new ErrorDataResult<AnimalSpecies>(null);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation($"{Role.AnimalSpeciesGetAll},{Role.Admin},{Role.User},{Role.SuperAdmin}")]
        [LogAspect(typeof(DatabaseLogger))]
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