using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace DataAccess.Concretes
{
    public class EfAdvertCategoryDal : EfEntityRepositoryBase<AdvertCategory, AppDbContext>, IAdvertCategoryDal
    {
    }
}
