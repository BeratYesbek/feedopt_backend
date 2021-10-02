using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;

namespace Business.Concretes
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        [SecuredOperation("UserOperationClaim.Add,SuperUser")]
        [ValidationAspect(typeof(UserOperationClaimValidator))]
        [PerformanceAspect(5)]
        public IDataResult<UserOperationClaim> Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessDataResult<UserOperationClaim>(null, "");
        }

        [SecuredOperation("UserOperationClaim.Update,SuperUser")]
        [ValidationAspect(typeof(UserOperationClaimValidator))]
        [PerformanceAspect(5)]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult();
        }
        [SecuredOperation("UserOperationClaim.Delete,SuperUser")]
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult();
        }
        [SecuredOperation("UserOperationClaim.Get,SuperUser")]
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
        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            var data = _userOperationClaimDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<UserOperationClaim>>(data);
            }

            return new ErrorDataResult<List<UserOperationClaim>>(null);
        }
    }
}