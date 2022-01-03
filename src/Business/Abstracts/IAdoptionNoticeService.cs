using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;
using Entity.Dtos;

namespace Business.Abstracts
{
    public interface IAdoptionNoticeService
    {
        IDataResult<AdoptionNotice> Add(AdoptionNotice adoptionNotice);

        IResult Update(AdoptionNotice adoptionNotice);

        IResult Delete(AdoptionNotice adoptionNotice);

        IDataResult<AdoptionNotice> Get(int id);

        IDataResult<List<AdoptionNotice>> GetAll();

        IDataResult<List<AdoptionNoticeDto>> GetAllAdoptionNoticeDetail();

        IDataResult<AdoptionNoticeDto> GetAdoptionNoticeDetailById(int id);

    }
}
