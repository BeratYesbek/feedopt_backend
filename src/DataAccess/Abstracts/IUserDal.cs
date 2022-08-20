using System.Collections.Generic;
using Core.DataAccess;
using Core.Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);

        List<UserDto> GetUserDetails();

    }
}