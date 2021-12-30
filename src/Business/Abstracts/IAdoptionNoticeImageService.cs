using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Business.Abstracts
{
    public interface IAdoptionNoticeImageService
    {
        IResult Add(AdoptionNoticeImage adoptionNoticeImage, IFormFile[] formFiles);

        IResult Update(AdoptionNoticeImage[] adoptionNoticeImage, IFormFile[] formFiles);

        IResult Delete(AdoptionNoticeImage[] adoptionNoticeImage);

        IDataResult<List<AdoptionNoticeImage>> GetByAdoptionNoticeId(int id);

        IDataResult<AdoptionNoticeImage> Get(int id);

        IDataResult<List<AdoptionNoticeImage>> GetAll();
    }
}