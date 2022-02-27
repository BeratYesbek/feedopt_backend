using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Security.Role;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
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
        
        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.LocationAdd}")]
        [ValidationAspect(typeof(LocationValidator))]
        [CacheRemoveAspect("ILocationService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<Location> Add(Location location)
        {
            var data = _locationDal.Add(location);
            return new SuccessDataResult<Location>(data);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.LocationUpdate}")]
        [ValidationAspect(typeof(LocationValidator))]
        [CacheRemoveAspect("ILocationService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Update(Location location)
        {
            _locationDal.Update(location);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.LocationDelete}")]
        [CacheRemoveAspect("ILocationService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(Location location)
        {
            _locationDal.Delete(location);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.LocationGet}")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<Location> Get(int id)
        {
            var data = _locationDal.Get(l => l.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<Location>(data);
            }

            return new ErrorDataResult<Location>(null);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.LocationGetAll}")]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect(typeof(DatabaseLogger))]
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