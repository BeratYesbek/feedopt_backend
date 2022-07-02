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
    /// <summary>
    /// This method manage colors. Whenever need to manage colors should do in this class because of SOLID - S => single responsibility principle
    /// </summary>
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        /// <summary>
        /// Color is added by this method
        /// </summary>
        /// <param name="color"></param>
        /// <returns>IDataResult</returns>
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

        /// <summary>
        /// Color is updated by this method
        /// </summary>
        /// <param name="color"></param>
        /// <returns>IResult</returns>
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
        
        /// <summary>
        /// Color is deleted by this method
        /// </summary>
        /// <param name="color"></param>
        /// <returns>IResult</returns>
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

        /// <summary>
        /// This method get single color by Color ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDataResult</returns>
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

        /// <summary>
        /// This method get all colors 
        /// </summary>
        /// <returns>IDataResult</returns>
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
