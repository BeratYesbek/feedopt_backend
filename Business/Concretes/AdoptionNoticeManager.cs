using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Concretes;
using Entity.concretes;

namespace Business.Concretes
{
    public class AdoptionNoticeManager : IAdoptionNoticeService
    {
        private EfAdoptionNoticeDal adoptionNoticeDal = new EfAdoptionNoticeDal();

        public IResult Add(AdoptionNotice adoptionNotice)
        {
            adoptionNoticeDal.Add(adoptionNotice);
            return new SuccessResult();
        }

        public IResult Update(AdoptionNotice adoptionNotice)
        {
            adoptionNoticeDal.Update(adoptionNotice);
            return new SuccessResult();
        }

        public IResult Delete(AdoptionNotice adoptionNotice)
        {
            adoptionNoticeDal.Delete(adoptionNotice);
            return new SuccessResult();
        }

        public IDataResult<AdoptionNotice> Get(int id)
        {
            var data = adoptionNoticeDal.Get(a => a.AdoptionNoticeId == id);
            if (data != null)
            {
                return new SuccessDataResult<AdoptionNotice>(data);
            }

            return new ErrorDataResult<AdoptionNotice>(null);
        }

        public IDataResult<List<AdoptionNotice>> GetAll()
        {
            var data = adoptionNoticeDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdoptionNotice>>(data);
            }

            return new ErrorDataResult<List<AdoptionNotice>>(null);
        }
    }
}