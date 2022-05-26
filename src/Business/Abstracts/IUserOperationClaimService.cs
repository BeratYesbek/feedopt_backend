using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;

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