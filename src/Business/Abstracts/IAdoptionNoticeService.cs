using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;

namespace Business.Abstracts
{
    public interface IAdoptionNoticeService
    {
        IDataResult<AdoptionNotice> Add(AdoptionNotice adoptionNotice);

        IResult Update(AdoptionNotice adoptionNotice);

        IResult Delete(AdoptionNotice adoptionNotice);

        IDataResult<AdoptionNotice> Get(int id);

        IDataResult<List<AdoptionNotice>> GetAll();
    }
}
