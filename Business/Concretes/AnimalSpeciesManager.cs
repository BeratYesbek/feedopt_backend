using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
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

        [ValidationAspect(typeof(AnimalSpeciesValidator))]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll")]
        [PerformanceAspect(5)]
        public IResult Add(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Add(animalSpecies);
            return new SuccessResult();
        }

        [ValidationAspect(typeof(AnimalSpeciesValidator))]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll")]
        [PerformanceAspect(5)]
        public IResult Update(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Update(animalSpecies);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll")]
        public IResult Delete(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Delete(animalSpecies);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        public IDataResult<AnimalSpecies> Get(int id)
        {
            var data = _animalSpeciesDal.Get(a => a.AnimalSpeciesId == id);
            if (data != null)
            {
                return new SuccessDataResult<AnimalSpecies>(data);
            }

            return new ErrorDataResult<AnimalSpecies>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
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