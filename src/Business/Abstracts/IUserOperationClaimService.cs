using System.Collections.Generic;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;

namespace Business.Abstracts
{
    public interface IUserOperationClaimService
    {
        IDataResult<UserOperationClaim> Add(UserOperationClaim userOperationClaim);

        IDataResult<UserOperationClaim> AddDefaultRole(User user);

        IResult Update(UserOperationClaim userOperationClaim);

        IResult Delete(UserOperationClaim userOperationClaim);

        IDataResult<UserOperationClaim> Get(int id);

        IDataResult<List<UserOperationClaim>> GetAll();
    }
}