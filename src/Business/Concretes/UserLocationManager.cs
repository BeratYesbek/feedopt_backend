using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect.SecurityAspect;
using Business.Security.Role;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    public class UserLocationManager : IUserLocationService
    {
        private readonly IUserLocationDal _locationDal;
        public UserLocationManager(IUserLocationDal locationDal)
        {
            _locationDal = locationDal;
        }

        [SecuredOperation($"{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(UserLocationValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IUserLocationService.GetAll",Priority = 5)]
        [CacheRemoveAspect("IUserLocationService.GetByUserId",Priority = 6)]
        public IDataResult<UserLocation> Add(UserLocation location)
        {
            location.UserId = CurrentUser.User.Id; 
            var data = _locationDal.Add(location);
            if (data != null)
            {
                return new SuccessDataResult<UserLocation>(data);
            }

            return new ErrorDataResult<UserLocation>(null);
        }
        
        [SecuredOperation($"{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheAspect(Priority = 5)]
        public IDataResult<UserLocation> GetByUserId(int userId)
        {
            var data = _locationDal.Get(t => t.UserId == userId);
            if (data != null)
            {
                return new SuccessDataResult<UserLocation>(data);
            }

            return new ErrorDataResult<UserLocation>(null);
        }

        [SecuredOperation($"{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheAspect(Priority = 5)]
        public IDataResult<List<UserLocation>> GetAll()
        {
            var data = _locationDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<UserLocation>>(data);
            }

            return new ErrorDataResult<List<UserLocation>>(null);
        }

        [SecuredOperation($"{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [ValidationAspect(typeof(UserLocationValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        [CacheRemoveAspect("IUserLocationService.GetAll",Priority = 5)]
        [CacheRemoveAspect("IUserLocationService.GetByUserId",Priority = 6)]
        public async Task<IDataResult<UserLocation>> AddAsync(UserLocation userLocation)
        {
            var data = await _locationDal.AddAsync(userLocation);
            if (data is not null)
            {
                return new SuccessDataResult<UserLocation>(data);
            }

            return new ErrorDataResult<UserLocation>(null);
        }
    }
}
