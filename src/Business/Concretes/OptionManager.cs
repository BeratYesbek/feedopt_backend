﻿using Business.Abstracts;
using Business.BusinessAspect.SecurityAspect;
using Business.Security.Role;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Dtos;
namespace Business.Concretes
{
    public class OptionManager : IOptionService
    {

        private readonly IOptionDal _optionDal;
        public OptionManager(IOptionDal optionDal)
        {
            _optionDal = optionDal;
        }

        [SecuredOperation($"{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        [PerformanceAspect(5, Priority = 4)]
        [CacheAspect(Priority = 5)]
        public IDataResult<OptionDto> GetOptions()
        {
            return new SuccessDataResult<OptionDto>(_optionDal.GetOptions());
        }
    }
}
