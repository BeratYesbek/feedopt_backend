using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Business.Concretes
{
    /// <summary>
    /// This class manage Age Ranges. Whenever need to manage something on that, everything should do in this class because of SOLID - Single Responsibility Principle
    /// </summary>
    public class AgeRangeManager : IAgeRangeService
    {
        private readonly IAgeRangeDal _ageRangeDal;

        public AgeRangeManager(IAgeRangeDal ageRangeDal)
        {
            _ageRangeDal = ageRangeDal;
        }

        /// <summary>
        /// Age ranges is added by this method. It is going to work with O(2) 
        /// </summary>
        /// <param name="age"></param>
        /// <returns>IResult</returns>
        [SecuredOperation($"{Role.AgeRangesAdd},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(AgeRangesValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IAgeRangeService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IAgeRangeService.Get", Priority = 6)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 7)]
        public IResult Add(Age age)
        {
            _ageRangeDal.Add(age);
            return new SuccessResult();
        }

        /// <summary>
        /// Age ranges is updated by this method. It is going to work with O(2)
        /// </summary>
        /// <param name="age"></param>
        /// <returns>IResult</returns>
        [SecuredOperation($"{Role.AgeRangesUpdate},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(AgeRangesValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IAgeRangeService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IAgeRangeService.Get", Priority = 6)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 7)]
        public IResult Update(Age age)
        {
            _ageRangeDal.Update(age);
            return new SuccessResult();
        }

        /// <summary>
        /// Age ranges is deleted by this method. It is going to work with O(2)
        /// </summary>
        /// <param name="age"></param>
        /// <returns>IResult</returns>
        [SecuredOperation($"{Role.AgeRangesDelete},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheRemoveAspect("IAgeRangeService.GetAll", Priority = 4)]
        [CacheRemoveAspect("IAgeRangeService.Get", Priority = 5)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 6)]
        public IResult Delete(Age age)
        {
            _ageRangeDal.Delete(age);
            return new SuccessResult();
        }

        /// <summary>
        /// This method get age range by ID. It is going to work O(2)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.AgeRangesGet},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<Age> Get(int id)
        {
            var data = _ageRangeDal.Get(a => a.Id == id);
            return new SuccessDataResult<Age>(data);
        }

        /// <summary>
        /// This method get all age ranges. It is going to work O(2)
        /// </summary>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.AgeRangesGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<List<Age>> GetAll()
        {
            var data = _ageRangeDal.GetAll();
            return data.Count > 0 ? new SuccessDataResult<List<Age>>(data) : new SuccessDataResult<List<Age>>(null);
        }
    }
}
