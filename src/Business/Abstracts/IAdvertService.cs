using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;
using Entity.Concretes;
using Entity.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Abstracts
{
    public interface IAdvertService
    {
        Task<IDataResult<Advert>> Add(Advert advert, AdvertImage advertImage, IFormFile[] files, Location location);

        Task<IResult> Update(Advert advert, AdvertImage advertImage,IFormFile[] files, Location location);

        Task<IResult> Delete(Advert advert);

        IDataResult<Advert> Get(int id);

        IDataResult<List<AdvertReadDto>> GetAllAdvertDetail(int pageNumber);

        IDataResult<List<AdvertReadDto>> GetAllAdvertDetailsByFilter(int pageNumber);

        IDataResult<AdvertReadDto> GetAdvertDetailById(int id);

        IDataResult<List<Advert>> GetAll();
    }
}
