using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Filters;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entity.Concretes;
using Core.Utilities.Cloud.Aws.S3;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Dtos;
using Entity.Dtos.Filter;

namespace Business.Concretes
{
    public class UserManager : UserFilter,IUserService
    {
        private readonly IUserDal _userDal;

        private readonly IS3AmazonService _s3AWsService;

        public UserManager(IUserDal userDal,IS3AmazonService s3AmazonService) : this(userDal)
        {
            _s3AWsService = s3AmazonService;
        }
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IDashboardService.GetDashboard")]
        public IDataResult<User> Add(User user)
        {
            var result = _userDal.Add(user);

            if (result != null)
            {
                return new SuccessDataResult<User>(user);
            }

            return new ErrorDataResult<User>(null);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        public async Task<IResult> Update(User user)
        {
            if (user.File is not null)
            {
                var imageResult = await _s3AWsService.UploadAsync(user.File,FileExtension.ImageExtension);
                if (imageResult.Success)
                {
                    user.ImagePath = imageResult.Message.Split("&&")[0];
                    _userDal.Update(user);
                    return new SuccessResult();
                }
                else
                {
                    return new ErrorResult();
                }
            }
            _userDal.Update(user);

            return new SuccessResult();
        }


        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }


        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        public IDataResult<User> Get(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<User>(result);
            }

            return new ErrorDataResult<User>(result);
        }


        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<User>>(result);
            }

            return new ErrorDataResult<List<User>>(null);
        }


        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        public IDataResult<User> GetByMail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);
            if (result != null)
            {
                return new SuccessDataResult<User>(result);
            }

            return new ErrorDataResult<User>(null);
        }

        public IResult UpdateLocation(decimal latitude, decimal longitude, int userId)
        {
            var user = _userDal.Get(t => t.Id == userId);
            if (user != null)
            {

                _userDal.Update(user);
            }

            return new SuccessResult();
        }

        public IDataResult<List<UserDto>> GetUserDetails(UserFilterDto filter)
        {
            Expression<Func<User, bool>> filters = c => true;

            var properties = filter.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(filter, null);
                if (value?.GetType() == typeof(int[]) && value != null)
                {
                    int[] arrayInteger = (int[])value;
                    if (arrayInteger.Length > 0)
                    {
                        filters = (Expression<Func<User, bool>>)StartFilterInvokeMethod(filters, value, property);
                    }
                }
                else if (value?.GetType() == typeof(bool) && value != null)
                {
                    var data = (bool)value;
                    filters = (Expression<Func<User, bool>>)StartFilterInvokeMethod(filters, value, property);
                }
            }
            var users = _userDal.GetUserDetails(filters);
            
            return new SuccessDataResult<List<UserDto>>(users);
        }
        
        private object StartFilterInvokeMethod(Expression<Func<User, bool>> filters, object value, PropertyInfo property)
        {
            // object must include filters type of expression  and value, Value might be any type depend on situations
            object[] methodParams = { filters, value };

            // AdvertManager inherited Advert Filter class and AdvertFilter class inherited BaseFilterInvoke class 
            // Base filter class include GetInvokeMethod that is taking few properties, one of it is name of method that run
            // first parameter methodName it must includes property name of AdvertFilterDto and filter method's is going to work 
            return (Expression<Func<User, bool>>)GetInvokeMethod($"{property.Name}Condition", methodParams);
        }
    }
}