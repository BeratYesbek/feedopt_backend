using Core.DataAccess;
using Core.Entity.Concretes;
using DataAccess.Abstracts;

namespace DataAccess.Concretes
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim,AppDbContext>, IOperationClaimDal
    {
    }
}
