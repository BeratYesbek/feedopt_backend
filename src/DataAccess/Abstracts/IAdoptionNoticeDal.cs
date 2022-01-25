using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity.concretes;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IAdoptionNoticeDal : IEntityRepository<AdoptionNotice>
    {
        List<AdoptionNoticeDto> GetAllAdoptionNoticeDetail(int pageNumber, int pageSize = 20);
        List<AdoptionNoticeDto> GetAdoptionNoticeDetailsByFilter(Expression<Func<AdoptionNotice, bool>> filter, int pageNumber, int pageSize = 10);
        AdoptionNoticeDto GetAdoptionNoticeDetailById(int id);

    }
}
