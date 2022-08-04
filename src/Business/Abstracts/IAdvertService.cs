using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
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

        Task<IResult> Update(Advert advert, AdvertImage advertImage, IFormFile[] files, int[] deletedImages, Location location);

        IResult Delete(Advert advert);

        IDataResult<Advert> Get(int id);

        IDataResult<List<AdvertReadDto>> GetAllAdvertDetail(int pageNumber);

        IDataResult<List<AdvertReadDto>> GetAllAdvertDetailsByFilter(AdvertFilterDto advertFilterDto, int pageNumber);

        IDataResult<AdvertReadDto> GetAdvertDetailById(int id);

        IDataResult<List<AdvertReadDto>> GetAllAdvertByDistance(int pageNumber);

        IDataResult<List<AdvertReadDto>> GetAdvertDetailByUserId(int userId, int pageNumber);

        IDataResult<List<Advert>> GetAll(Expression<Func<Advert,bool>> filter=null);

        IDataResult<AdvertEditDto> Edit(int id);

        IResult UpdateStatus(Advert advert);
    }
}