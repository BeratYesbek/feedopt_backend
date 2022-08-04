using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace DataAccess.Concretes
{
    public class EfAdvertImageDal : EfEntityRepositoryBase<AdvertImage, AppDbContext>,IAdvertImageDal
    {
    }
}
