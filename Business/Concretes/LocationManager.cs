using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entity.Concretes;

namespace Business.Concretes
{
    public class LocationManager : ILocationService
    {

        private readonly ILocationDal _locationDal;

        public LocationManager(ILocationDal locationDal)
        {
            _locationDal = locationDal;
        }

        [ValidationAspect(typeof(LocationValidator))]
        [CacheRemoveAspect("ILocationService.GetAll")]
        [PerformanceAspect(5)]
        public IDataResult<Location> Add(Location location)
        {
            var data = _locationDal.Add(location);
            return new SuccessDataResult<Location>(data);
        }

        [ValidationAspect(typeof(LocationValidator))]
        [CacheRemoveAspect("ILocationService.GetAll")]
        [PerformanceAspect(5)]
        public IResult Update(Location location)
        {
            _locationDal.Update(location);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ILocationService.GetAll")]
        [PerformanceAspect(5)]
        public IResult Delete(Location location)
        {
            _locationDal.Delete(location);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        public IDataResult<Location> Get(int id)
        {
            var data = _locationDal.Get(l => l.LocationId == id);
            if (data != null)
            {
                return new SuccessDataResult<Location>(data);
            }

            return new ErrorDataResult<Location>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Location>> GetAll()
        {
            var data = _locationDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Location>>(data);
            }

            return new ErrorDataResult<List<Location>>(null);
        }
    }
}