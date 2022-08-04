using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace DataAccess.Concretes
{
    public class EfAgeRangeDal : EfEntityRepositoryBase<Age,AppDbContext>,IAgeRangeDal
    {
    }
}
