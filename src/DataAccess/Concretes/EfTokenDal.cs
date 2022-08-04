using Core.DataAccess;
using Core.Entity.Concretes;
using DataAccess.Abstracts;

namespace DataAccess.Concretes
{
    public class EfTokenDal : EfEntityRepositoryBase<Token,AppDbContext>,ITokenDal
    {
    }
}
