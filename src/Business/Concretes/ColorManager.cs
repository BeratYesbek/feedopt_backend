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
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;


        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [SecuredOperation($"{Role.ColorAdd},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [CacheRemoveAspect("IColorService.GetAll", Priority = 3)]
        [CacheRemoveAspect("IColorService.Get", Priority = 4)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 5)]
        [PerformanceAspect(5, Priority = 5)]
        public IDataResult<Color> Add(Color color)
        {
            return new SuccessDataResult<Color>(_colorDal.Add(color));
        }

        [SecuredOperation($"{Role.ColorUpdate},{Role.Admin},{Role.SuperAdmin}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [CacheRemoveAspect("IColorService.GetAll", Priority = 3)]
        [CacheRemoveAspect("IColorService.Get", Priority = 4)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 5)]
        [PerformanceAspect(5, Priority = 5)]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.ColorDelete},{Role.Admin},{Role.SuperAdmin}",Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [CacheRemoveAspect("IColorService.GetAll",Priority =3)]
        [CacheRemoveAspect("IColorService.Get",Priority =4)]
        [CacheRemoveAspect("IOptionService.GetOptions", Priority = 6)]
        [PerformanceAspect(5, Priority = 5)]
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult();
        }

        [SecuredOperation($"{Role.ColorGet},{Role.User},{Role.Admin},{Role.SuperAdmin}")]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<Color> Get(int id)
        {
            var data = _colorDal.Get(a => a.Id == id);
            if (data is not null)
            {
                return new SuccessDataResult<Color>(data);
            }
            return new ErrorDataResult<Color>(null);
        }
        [SecuredOperation($"{Role.ColorGetAll},{Role.User},{Role.Admin},{Role.SuperAdmin}",Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [PerformanceAspect(5,Priority = 3)]
        [CacheAspect(Priority = 4)]
        public IDataResult<List<Color>> GetAll()
        {
            var data = _colorDal.GetAll(null,true,
                t => t.ColorTranslations.Where(t => t.CultureName.Equals(CurrentUser.CultureName)));
            return new SuccessDataResult<List<Color>>(data);
       
        }
    }
}
