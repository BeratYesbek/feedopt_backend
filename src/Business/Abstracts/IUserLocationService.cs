using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface IUserLocationService
    {
        IDataResult<UserLocation> Add(UserLocation location);
        
        IDataResult<UserLocation> GetByUserId(int userId);

        IDataResult<List<UserLocation>> GetAll();

        Task<IDataResult<UserLocation>> AddAsync(UserLocation userLocation);

    }
}
