using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface IAdoptionNoticeImageService
    {
        IResult Add(AdoptionNoticeImage adoptionNoticeImage);

        IResult Update(AdoptionNoticeImage adoptionNoticeImage);

        IResult Delete(AdoptionNoticeImage adoptionNoticeImage);

        IDataResult<AdoptionNoticeImage> Get(int id);

        IDataResult<List<AdoptionNoticeImage>> GetAll();
    }
}
