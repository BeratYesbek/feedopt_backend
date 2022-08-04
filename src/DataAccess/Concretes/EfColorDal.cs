using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace DataAccess.Concretes
{
    public class EfColorDal : EfEntityRepositoryBase<Color, AppDbContext>, IColorDal
    {
    }
}
