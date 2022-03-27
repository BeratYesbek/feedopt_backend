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
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
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

        [CacheRemoveAspect("IColorService.GetAll")]
        [ValidationAspect(typeof(ColorValidator))]
        [SecuredOperation($"{Role.Admin},{Role.SuperAdmin}")]
        public IDataResult<Color> Add(Color color)
        {
            return new SuccessDataResult<Color>(_colorDal.Add(color));
        }

        [CacheRemoveAspect("IColorService.GetAll")]
        [ValidationAspect(typeof(ColorValidator))]
        [SecuredOperation($"{Role.Admin},{Role.SuperAdmin}")]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IColorService.GetAll")]
        [SecuredOperation($"{Role.Admin},{Role.SuperAdmin}")]
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult();
        }

        
        public IDataResult<Color> Get(int id)
        {
            var data = _colorDal.Get(a => a.Id == id);
            if (data is not null)
            {
                return new SuccessDataResult<Color>(data);
            }
            return new ErrorDataResult<Color>(null);
        }
        
        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            var data = _colorDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Color>>(data);
            }
            return new ErrorDataResult<List<Color>>(null);
        }
    }
}
