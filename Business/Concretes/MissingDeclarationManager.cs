using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entity;

namespace Business.Concretes
{
    public class MissingDeclarationManager : IMissingDeclarationService
    {
        private readonly IMissingDeclarationDal _missingDeclarationDal;

        public MissingDeclarationManager(IMissingDeclarationDal missingDeclarationDal)
        {
            _missingDeclarationDal = missingDeclarationDal;
        }

        [ValidationAspect(typeof(MissingDeclarationValidator))]
        [CacheRemoveAspect("IMissingDeclarationService.GetAll")]
        [SecuredOperation("MissingDeclaration.Add,User")]
        [PerformanceAspect(5)]
        public IDataResult<MissingDeclaration> Add(MissingDeclaration missingDeclaration)
        {
            var data = _missingDeclarationDal.Add(missingDeclaration);
            return new SuccessDataResult<MissingDeclaration>(data);
        }

        [ValidationAspect(typeof(MissingDeclarationValidator))]
        [CacheRemoveAspect("IMissingDeclarationService.GetAll")]
        [SecuredOperation("MissingDeclaration.Update,User")]
        [PerformanceAspect(5)]
        public IResult Update(MissingDeclaration missingDeclaration)
        {
            _missingDeclarationDal.Update(missingDeclaration);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [CacheRemoveAspect("IMissingDeclarationService.GetAll")]
        [SecuredOperation("MissingDeclaration.Delete,User")]
        public IResult Delete(MissingDeclaration missingDeclaration)
        {
            _missingDeclarationDal.Delete(missingDeclaration);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [SecuredOperation("MissingDeclaration.Get,User")]
        public IDataResult<MissingDeclaration> Get(int id)
        {
            var data = _missingDeclarationDal.Get(m => m.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<MissingDeclaration>(data);
            }

            return new ErrorDataResult<MissingDeclaration>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("MissingDeclaration.GetAll,User")]
        public IDataResult<List<MissingDeclaration>> GetAll()
        {
            var data = _missingDeclarationDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<MissingDeclaration>>(data);
            }

            return new ErrorDataResult<List<MissingDeclaration>>(null);
        }
    }
}