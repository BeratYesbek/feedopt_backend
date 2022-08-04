using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IAdvertDal : IEntityRepository<Advert>
    {
        List<AdvertReadDto> GetAllAdvertDetail(int pageNumber, double latitude, double longitude, int userId, double diameter = 30, int pageSize = 30);
        List<AdvertReadDto> GetAllAdvertDetailsByFilter(Expression<Func<Advert, bool>> filter, int userId, double latitude, double longitude, int pageNumber, int distance = 1000000, int pageSize = 30);
        List<AdvertReadDto> GetAllAdvertByDistance(double latitude, double longitude, int userId, int pageNumber, double diameter = 30, int pageSize = 30);
        AdvertReadDto GetAdvertDetailById(int id, int userId, double longitude, double latitude);
        AdvertEditDto Edit(int id);
    }
}
