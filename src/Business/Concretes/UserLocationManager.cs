using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    public class UserLocationManager : IUserLocationService
    {
        public readonly IUserLocationDal _locationDal;
        public UserLocationManager(IUserLocationDal locationDal)
        {
            _locationDal = locationDal;
        }

        public IDataResult<UserLocation> Add(UserLocation location)
        {
            var data = _locationDal.Add(location);
            if (data != null)
            {
                return new SuccessDataResult<UserLocation>(data);
            }

            return new ErrorDataResult<UserLocation>(null);
        }

        public IResult Update(UserLocation location)
        {
            _locationDal.Update(location);
            return new SuccessResult();
        }

        public IResult Delete(UserLocation location)
        {
            _locationDal.Delete(location);
            return new SuccessResult();
        }

        public IDataResult<UserLocation> Get(int id)
        {
            var data = _locationDal.Get(x => x.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<UserLocation>(data);
            }

            return new ErrorDataResult<UserLocation>(null);
        }

        public IDataResult<List<UserLocation>> GetAll()
        {
            var data = _locationDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<UserLocation>>(data);
            }

            return new ErrorDataResult<List<UserLocation>>(null);
        }
    }
}
