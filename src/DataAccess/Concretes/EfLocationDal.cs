using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace DataAccess.Concretes
{
    public class EfLocationDal : EfEntityRepositoryBase<Location, AppDbContext>, ILocationDal
    {
    }
}