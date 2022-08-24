using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Entity.Dtos;
using Entity.Dtos.Filter;

namespace Business.Abstracts
{
    public interface IUserService
    {
        IDataResult<User> Add(User user);

        Task<IResult> Update(User user);

        IResult Delete(User user);

        IDataResult<User> Get(int id);

        IDataResult<List<User>> GetAll();

        List<OperationClaim> GetClaims(User user);

        IDataResult<User> GetByMail(string email);

        IResult UpdateLocation(decimal latitude, decimal longitude,int userId);
        
        IDataResult<List<UserDto>> GetUserDetails(UserFilterDto filterDto);


    }
}