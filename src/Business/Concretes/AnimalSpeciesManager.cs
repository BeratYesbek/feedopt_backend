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

        /// <summary>
        /// Animal Species is added by this method
        /// </summary>
        /// <param name="animalSpecies"></param>
        /// <returns>IResult</returns>
        [SecuredOperation($"{Role.AnimalSpeciesAdd},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [ValidationAspect(typeof(AnimalSpeciesValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAllByAnimalCategoryId", Priority = 6)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 7)]
        public IResult Add(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Add(animalSpecies);
            return new SuccessResult();
        }

        /// <summary>
        /// Animal Species is updated by this method
        /// </summary>
        /// <param name="animalSpecies"></param>
        /// <returns>IResult</returns>
        [SecuredOperation($"{Role.AnimalSpeciesUpdate},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [ValidationAspect(typeof(AnimalSpeciesValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAllByAnimalCategoryId", Priority = 6)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 7)]
        public IResult Update(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Update(animalSpecies);
            return new SuccessResult();
        }

        /// <summary>
        /// Animal Species is deleted by this method
        /// </summary>
        /// <param name="animalSpecies"></param>
        /// <returns>IResult</returns>
        [SecuredOperation($"{Role.AnimalSpeciesDelete},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAll", Priority = 4)]
        [CacheRemoveAspect("IAnimalSpeciesService.GetAllByAnimalCategoryId", Priority = 5)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 6)]

        public IResult Delete(AnimalSpecies animalSpecies)
        {
            _animalSpeciesDal.Delete(animalSpecies);
            return new SuccessResult();
        }

        /// <summary>
        /// This method get all animal species by animal category ID
        /// </summary>
        /// <param name="animalCategoryId"></param>
        /// <returns>IDataResult<list type="AnimalSpecies"></list></returns>
        [SecuredOperation($"{Role.AnimalSpeciesGetAll},{Role.Admin},{Role.User},{Role.SuperAdmin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<List<AnimalSpecies>> GetAllByAnimalCategoryId(int animalCategoryId)
        {
            var data = _animalSpeciesDal.GetAll(a => a.AnimalCategoryId == animalCategoryId);
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AnimalSpecies>>(data);
            }
            return new ErrorDataResult<List<AnimalSpecies>>(null);
        }

        /// <summary>
        /// This method get single animal species by  ID
        /// </summary>
        /// <param name="animalCategoryId"></param>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.AnimalSpeciesGet},{Role.Admin},{Role.User},{Role.SuperAdmin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<AnimalSpecies> Get(int id)
        {
            var data = _animalSpeciesDal.Get(a => a.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<AnimalSpecies>(data);
            }

            return new ErrorDataResult<AnimalSpecies>(null);
        }
        /// <summary>
        /// This method get all animal species
        /// </summary>
        /// <returns>IDataResult<list type="AnimalSpecies"></list></returns>
        [SecuredOperation($"{Role.AnimalSpeciesGetAll},{Role.Admin},{Role.User},{Role.SuperAdmin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
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