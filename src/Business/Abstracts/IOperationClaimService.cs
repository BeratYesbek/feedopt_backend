using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;

namespace Business.Abstracts
{
    public interface IOperationClaimService
    {
        IDataResult<OperationClaim> Add(OperationClaim operationClaim);

        IResult Update(OperationClaim operationClaim);

        IResult Delete(OperationClaim operationClaim);

        IDataResult<OperationClaim> Get(int id);

        IDataResult<List<OperationClaim>> GetAll();
    }
}
