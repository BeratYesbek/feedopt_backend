using System.Collections.Generic;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Security.Role;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;

namespace Business.Concretes
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [SecuredOperation($"{Role.OperationClaimAdd},{Role.SuperAdmin}", Priority = 1)]
        [ValidationAspect(typeof(OperationClaimValidator), Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [PerformanceAspect(5, Priority = 4)]
        [CacheRemoveAspect("IOperationClaimService.GetAll", Priority = 5)]
        public IDataResult<OperationClaim> Add(OperationClaim operationClaim)
        {
            var data = _operationClaimDal.Add(operationClaim);
            if (data != null)
            {
                return new SuccessDataResult<OperationClaim>(data);
            }

            return new ErrorDataResult<OperationClaim>(null);
        }

        [SecuredOperation($"{Role.OperationClaimUpdate},{Role.SuperAdmin}", Priority = 1)]
        [ValidationAspect(typeof(OperationClaimValidator), Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [PerformanceAspect(5, Priority = 4)]
        [CacheRemoveAspect("IOperationClaimService.GetAll", Priority = 5)]
        public IResult Update(OperationClaim operationClaim)
        {
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.OperationClaimDelete},{Role.SuperAdmin}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [CacheRemoveAspect("IOperationClaimService.GetAll", Priority = 4)]
        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult();
        }

        [LogAspect(typeof(DatabaseLogger), Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        public IDataResult<OperationClaim> Get(int id)
        {
            var data = _operationClaimDal.Get(t => t.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<OperationClaim>(data);
            }

            return new ErrorDataResult<OperationClaim>(null);
        }

        [SecuredOperation($"{Role.OperationClaimDelete},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<List<OperationClaim>> GetAll()
        {
            var data = _operationClaimDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<OperationClaim>>(data);
            }

            return new ErrorDataResult<List<OperationClaim>>(null);
        }


        [LogAspect(typeof(DatabaseLogger), Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        public IDataResult<OperationClaim> GetByName(string name)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(t => t.Name == name));
        }
    }
}
