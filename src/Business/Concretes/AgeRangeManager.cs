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
    public class AgeRangeManager : IAgeRangeService
    {
        private readonly IAgeRangeDal _ageRangeDal;

        public AgeRangeManager(IAgeRangeDal ageRangeDal)
        {
            _ageRangeDal = ageRangeDal;
        }

        [SecuredOperation($"{Role.AgeRangesAdd},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(AgeRangesValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IAgeRangeService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IAgeRangeService.Get", Priority = 6)]
        public IResult Add(Age age)
        {
            _ageRangeDal.Add(age);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.AgeRangesUpdate},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(AgeRangesValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IAgeRangeService.GetAll", Priority = 5)]
        [CacheRemoveAspect("IAgeRangeService.Get", Priority = 6)]
        public IResult Update(Age age)
        {
            _ageRangeDal.Update(age);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.AgeRangesDelete},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheRemoveAspect("IAgeRangeService.GetAll", Priority = 4)]
        [CacheRemoveAspect("IAgeRangeService.Get", Priority = 5)]
        public IResult Delete(Age age)
        {
            _ageRangeDal.Delete(age);
            return new ErrorResult();
        }

        [SecuredOperation($"{Role.AgeRangesGet},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<Age> Get(int id)
        {
            var data = _ageRangeDal.Get(a => a.Id == id);
            return new SuccessDataResult<Age>(data);
        }

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
