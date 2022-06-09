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
        
        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.LocationAdd}",Priority = 1)]
        [ValidationAspect(typeof(LocationValidator),Priority = 2)]
        [LogAspect(typeof(DatabaseLogger),Priority = 3)]
        [PerformanceAspect(5,Priority = 4)]
        [CacheRemoveAspect("ILocationService.GetAll",Priority = 5)]
        public IDataResult<Location> Add(Location location)
        {
            var data = _locationDal.Add(location);
            return new SuccessDataResult<Location>(data);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.LocationUpdate}", Priority = 1)]
        [ValidationAspect(typeof(LocationValidator), Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [PerformanceAspect(5, Priority = 4)]
        [CacheRemoveAspect("ILocationService.GetAll", Priority = 5)]
        public IResult Update(Location location)
        {
            _locationDal.Update(location);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.LocationDelete}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [PerformanceAspect(5, Priority = 4)]
        [CacheRemoveAspect("ILocationService.GetAll", Priority = 5)]
        public IResult Delete(Location location)
        {
            _locationDal.Delete(location);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.LocationGet}",Priority = 1)]
        [LogAspect(typeof(DatabaseLogger),Priority = 2)]
        [PerformanceAspect(5,Priority = 3)]
        public IDataResult<Location> Get(int id)
        {
            var data = _locationDal.Get(l => l.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<Location>(data);
            }

            return new ErrorDataResult<Location>(null);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.LocationGetAll}",Priority = 1)]
        [LogAspect(typeof(DatabaseLogger),Priority = 2)]
        [PerformanceAspect(5,Priority = 3)]
        [CacheAspect(Priority = 4)]
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