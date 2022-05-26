using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entity;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;

namespace Business.Concretes
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOperationClaimService _operationClaimService;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IOperationClaimService operationClaimService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimService = operationClaimService;
        }

        [SecuredOperation("UserOperationClaim.Add,SuperUser")]
        [ValidationAspect(typeof(UserOperationClaimValidator))]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<UserOperationClaim> Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessDataResult<UserOperationClaim>(null, "");
        }

        [SecuredOperation("UserOperationClaim.Update,SuperUser")]
        [ValidationAspect(typeof(UserOperationClaimValidator))]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult();
        }

        [SecuredOperation("UserOperationClaim.Delete,SuperUser")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult();
        }

        [SecuredOperation("UserOperationClaim.Get,SuperUser")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<UserOperationClaim> Get(int id)
        {
            var data = _userOperationClaimDal.Get(u => u.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<UserOperationClaim>(data);
            }

            return new ErrorDataResult<UserOperationClaim>(null);
        }

        [SecuredOperation("UserOperationClaim.GetAll,SuperUser")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            var data = _userOperationClaimDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<UserOperationClaim>>(data);
            }

            return new ErrorDataResult<List<UserOperationClaim>>(null);
        }

        public IDataResult<UserOperationClaim> AddDefaultRole(User user)
        {
            var operationClaim = _operationClaimService.GetByName("User").Data;
            if (operationClaim is not null)
            {
                var data = _userOperationClaimDal.Add(new UserOperationClaim
                {
                    UserId = user.Id,
                    OperationClaimId = operationClaim.Id
                });
                return new SuccessDataResult<UserOperationClaim>(data);
            }
            return new ErrorDataResult<UserOperationClaim>(null);

        }
    }
}