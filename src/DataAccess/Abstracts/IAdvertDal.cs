using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IAdvertDal : IEntityRepository<Advert>
    {
        List<AdvertReadDto> GetAllAdvertDetail(int pageNumber, double latitude, double longitude,double diameter = 30, int pageSize = 10);
        List<AdvertReadDto> GetAllAdvertDetailsByFilter(Expression<Func<Advert, bool>> filter, int pageNumber, int pageSize = 10);
        List<AdvertReadDto> GetAllAdvertByDistance(double latitude, double longitude, int pageNumber, double diameter = 30, int pageSize = 10);
        AdvertReadDto GetAdvertDetailById(int id);
    }
}
