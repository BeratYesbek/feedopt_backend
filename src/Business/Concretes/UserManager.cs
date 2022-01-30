using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entity;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        //[LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<User> Add(User user)
        {
            var result = _userDal.Add(user);

            if (result != null)
            {
                return new SuccessDataResult<User>(user);
            }

            return new ErrorDataResult<User>(null);
        }

        //[LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }


       //[LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }


      //  [LogAspect(typeof(FileLogger))]
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


      //  [LogAspect(typeof(FileLogger))]
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


      //  [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

       // [LogAspect(typeof(FileLogger))]
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
    }
}