using System.Collections.Generic;
using Business.Abstracts;
using Business.BusinessAspect.SecurityAspect;
using Business.Security.Role;
using Business.Validation.FluentValidation;
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
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOperationClaimService _operationClaimService;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IOperationClaimService operationClaimService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimService = operationClaimService;
        }

       /* [SecuredOperation($"{Role.UserOperationClaimAdd},{Role.SuperAdmin}",Priority = 1)]
        [ValidationAspect(typeof(UserOperationClaimValidator),Priority = 2)]
        [PerformanceAspect(5,Priority = 3)]
        [LogAspect(typeof(DatabaseLogger),Priority = 4)]*/
        public IDataResult<UserOperationClaim> Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessDataResult<UserOperationClaim>(null, "");
        }

        [SecuredOperation($"{Role.UserOperationClaimUpdate},{Role.SuperAdmin}", Priority = 1)]
        [ValidationAspect(typeof(UserOperationClaimValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.UserOperationClaimDelete},{Role.SuperAdmin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult();
        }


        [SecuredOperation($"{Role.UserOperationClaimGet},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        public IDataResult<UserOperationClaim> Get(int id)
        {
            var data = _userOperationClaimDal.Get(u => u.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<UserOperationClaim>(data);
            }

            return new ErrorDataResult<UserOperationClaim>(null);
        }

        [SecuredOperation($"{Role.UserOperationClaimGetAll},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
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