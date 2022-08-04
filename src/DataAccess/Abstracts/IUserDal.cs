using System.Collections.Generic;
using Core.DataAccess;
using Core.Entity.Concretes;

namespace DataAccess.Abstracts
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);

    }
}