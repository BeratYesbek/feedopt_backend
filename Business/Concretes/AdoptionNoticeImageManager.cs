using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    public class AdoptionNoticeImageManager : IAdoptionNoticeImageService
    {
        public IResult Add(AdoptionNoticeImage adoptionNoticeImage)
        {
            throw new NotImplementedException();
        }

        public IResult Update(AdoptionNoticeImage adoptionNoticeImage)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(AdoptionNoticeImage adoptionNoticeImage)
        {
            throw new NotImplementedException();
        }

        public IDataResult<AdoptionNoticeImage> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<AdoptionNoticeImage>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
