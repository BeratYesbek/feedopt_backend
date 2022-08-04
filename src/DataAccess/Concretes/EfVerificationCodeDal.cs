using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace DataAccess.Concretes
{
    public class EfVerificationCodeDal : EfEntityRepositoryBase<VerificationCode,AppDbContext> , IVerificationCodeDal
    {
    }
}
