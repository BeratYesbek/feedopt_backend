using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;
using Entity.Concretes;
using Entity.Dtos;
using Entity.Dtos.Filter;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Result.Abstracts.IResult;

namespace Business.Abstracts
{
    public interface IAdvertService
    {
        Task<IDataResult<Advert>> Add(Advert advert, AdvertImage advertImage, IFormFile[] files, Location location);

        Task<IResult> Update(Advert advert, AdvertImage advertImage, IFormFile[] files, Location location);

        Task<IResult> Delete(Advert advert);

        IDataResult<Advert> Get(int id);

        IDataResult<List<AdvertReadDto>> GetAllAdvertDetail(int pageNumber, double latitude, double longitude);

        IDataResult<List<AdvertReadDto>> GetAllAdvertDetailsByFilter(AdvertFilterDto advertFilterDto, int pageNumber);

        IDataResult<AdvertReadDto> GetAdvertDetailById(int id);

        IDataResult<List<AdvertReadDto>> GetAllAdvertByDistance(double latitude, double longitude, int pageNumber);

        IDataResult<List<AdvertReadDto>> GetAdvertDetailByUserId(int userId, int pageNumber);

        IDataResult<List<Advert>> GetAll();

        IResult UpdateStatus(Advert advert);
    }
}