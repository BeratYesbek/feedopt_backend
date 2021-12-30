using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
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
using Entity.concretes;

namespace Business.Concretes
{
    public class AdoptionNoticeManager : IAdoptionNoticeService
    {
        private readonly IAdoptionNoticeDal _adoptionNoticeDal;

        public AdoptionNoticeManager(IAdoptionNoticeDal adoptionNoticeDal)
        {
            _adoptionNoticeDal = adoptionNoticeDal;
        }

        [LogAspect(typeof(FileLogger))]
        //[ValidationAspect(typeof(AdoptionNoticeValidator))]
        //[SecuredOperation("AdoptionNotice.Add,User")]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdoptionNoticeService.GetAll")]
        public IDataResult<AdoptionNotice> Add(AdoptionNotice adoptionNotice)
        {
            return new SuccessDataResult<AdoptionNotice>(_adoptionNoticeDal.Add(adoptionNotice));
        }

        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdoptionNoticeService.GetAll")]
        [SecuredOperation("AdoptionNotice.Update,User")]
        [ValidationAspect(typeof(AdoptionNoticeValidator))]
        public IResult Update(AdoptionNotice adoptionNotice)
        {
            _adoptionNoticeDal.Update(adoptionNotice);
            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IAdoptionNoticeService.GetAll")]
        [SecuredOperation("AdoptionNotice.Delete,User")]
        [PerformanceAspect(5)]
        public IResult Delete(AdoptionNotice adoptionNotice)
        {
            _adoptionNoticeDal.Delete(adoptionNotice);
            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation("AdoptionNotice.Get,User")]
        public IDataResult<AdoptionNotice> Get(int id)
        {
            var data = _adoptionNoticeDal.Get(a => a.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<AdoptionNotice>(data);
            }

            return new ErrorDataResult<AdoptionNotice>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("AdoptionNotice.GetAll,User")]
        [CacheAspect]
        public IDataResult<List<AdoptionNotice>> GetAll()
        {
            var data = _adoptionNoticeDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdoptionNotice>>(data);
            }

            return new ErrorDataResult<List<AdoptionNotice>>(null);
        }
    }
}