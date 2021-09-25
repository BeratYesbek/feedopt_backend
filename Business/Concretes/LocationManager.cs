using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Concretes;
using Entity.Concretes;

namespace Business.Concretes
{
    public class LocationManager : ILocationService
    {
        private readonly EfLocationDal locationDal = new EfLocationDal();

        public IDataResult<Location> Add(Location location)
        {
            var data = locationDal.Add(location);
            return new SuccessDataResult<Location>(data);
        }

        public IResult Update(Location location)
        {
            locationDal.Update(location);
            return new SuccessResult();
        }

        public IResult Delete(Location location)
        {
            locationDal.Delete(location);
            return new SuccessResult();
        }

        public IDataResult<Location> Get(int id)
        {
            var data = locationDal.Get(l => l.LocationId == id);
            if (data != null)
            {
                return new SuccessDataResult<Location>(data);
            }

            return new ErrorDataResult<Location>(null);
        }

        public IDataResult<List<Location>> GetAll()
        {
            var data = locationDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Location>>(data);
            }

            return new ErrorDataResult<List<Location>>(null);
        }
    }
}