using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
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

        [ValidationAspect(typeof(AdoptionNoticeValidator))]
        [CacheRemoveAspect("IAdoptionNoticeService.GetAll")]
        [PerformanceAspect(5)]
        public IResult Add(AdoptionNotice adoptionNotice)
        {
            _adoptionNoticeDal.Add(adoptionNotice);
            return new SuccessResult();
        }
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdoptionNoticeService.GetAll")]
        [ValidationAspect(typeof(AdoptionNoticeValidator))]
        public IResult Update(AdoptionNotice adoptionNotice)
        {
            _adoptionNoticeDal.Update(adoptionNotice);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IAdoptionNoticeService.GetAll")]
        [PerformanceAspect(5)]
        public IResult Delete(AdoptionNotice adoptionNotice)
        {
            _adoptionNoticeDal.Delete(adoptionNotice);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        public IDataResult<AdoptionNotice> Get(int id)
        {
            var data = _adoptionNoticeDal.Get(a => a.AdoptionNoticeId == id);
            if (data != null)
            {
                return new SuccessDataResult<AdoptionNotice>(data);
            }

            return new ErrorDataResult<AdoptionNotice>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
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