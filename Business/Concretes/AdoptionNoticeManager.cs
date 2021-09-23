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
        public IResult Add(AdoptionNotice adoptionNotice)
        {
            var addedData = new EfAdoptionNoticeDal();
            addedData.Add(adoptionNotice);
            return new SuccessResult();
        }

        public IResult Update(AdoptionNotice adoptionNotice)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(AdoptionNotice adoptionNotice)
        {
            throw new NotImplementedException();
        }

        public IDataResult<AdoptionNotice> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<AdoptionNotice>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}